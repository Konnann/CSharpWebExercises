function startApp(){
    sessionStorage.clear();

    showHideMenuLinks();
    hideNotifications();
    showView("viewAppHome");

    const kinveyBaseUrl = "https://baas.kinvey.com/";
    const kinveyAppKey = "kid_Bkt5WGK4x";
    const kinveyAppSecret = '6561789fc1fb44f7b95ea82daf7eb1b9';
    const kinveyAppAuthHeaders = {
        'Authorization': "Basic " +
        btoa(kinveyAppKey + ":" + kinveyAppSecret)
    };

    // Attach event listeners
    $('#linkMenuAppHome').click(showHomeView);
    $('#linkMenuLogin').click(showLoginView);
    $('#linkMenuRegister').click(showRegisterView);
    $('#linkMenuUserHome').click(showUserHomeView);
    $('#linkMenuShop').click(showShopView);
    $('#linkMenuCart').click(showCartView);
    $('#linkMenuLogout').click(logout);
    $('#linkUserHomeShop').click(showShopView);
    $('#linkUserHomeCart').click(showCartView);


    // Bind the form submit buttons
    $("#formRegister").submit(register);
    $("#formLogin").submit(login);


    // User
    function register(event) {
        event.preventDefault();

        let userData = {
            username: $('#formRegister input[name=username]').val(),
            password: $('#formRegister input[name=password]').val(),
            name: $('#formRegister input[name=name]').val(),
            cart: {}
        };

        $.ajax({
            method: "POST",
            url: kinveyBaseUrl + "user/" + kinveyAppKey + "/",
            data: userData,
            headers: kinveyAppAuthHeaders,
            success: registerUserSuccess,
            error: failedRegistration
        });

        function registerUserSuccess(userInfo) {
            saveAuthInSession(userInfo);
            showHideMenuLinks();
            showUserHomeView();
            showInfo('User registration successful.͟');
        }

        function failedRegistration(response){
            handleAjaxError(response);
            $('#formRegister').trigger('reset');
        }
    }

    function login(event){
        event.preventDefault();
        let userData = {
            username: $('#formLogin input[name=username]').val(),
            password: $('#formLogin input[name=password]').val()
        };
        $.ajax({
            method: "POST",
            url: kinveyBaseUrl + "user/" + kinveyAppKey + "/login",
            data: userData,
            headers: kinveyAppAuthHeaders,
            success: loginUserSuccess,
            error: failedLogin
        });

        function loginUserSuccess(userInfo) {
            saveAuthInSession(userInfo);
            showHideMenuLinks();
            showUserHomeView();
            showInfo('Login successful.');
        }

        function failedLogin(response) {
            handleAjaxError(response);
            $('#formLogin').trigger('reset');
        }
    }
    
    function logout() {
        $.ajax({
            method: "POST",
            url: kinveyBaseUrl + "user/" + kinveyAppKey + "/_logout",
            headers: getUserAuthHeaders(),
            success: logoutSuccess,
            error: handleAjaxError
        });

        function logoutSuccess() {
            sessionStorage.clear();
            $('#loggedInUser').text('');
            showHideMenuLinks();
            showView('viewAppHome');
            showInfo('Logout Sucessful');
        }
    }


    // Products + Purchase logic
     function showAllProducts() {
         $('#shopProducts > table > tbody').empty();

         $.ajax({
             method: "GET",
             url: kinveyBaseUrl + "appdata/" + kinveyAppKey + "/products",
             headers: getUserAuthHeaders(),
             success: loadProductsSuccess,
             error: handleAjaxError
         });
         function loadProductsSuccess(products) {
             $('#shopProducts > table > tbody').empty();

             for (let item of products) {
                 let tr = $('<tr>');
                 displayTableRow(tr, item);
                 $('#shopProducts > table > tbody').append(tr);
             }

         }
         function displayTableRow(tr, item) {
             let purchase = $("<input type='button' value='Purchase'>")
                 .click( function() {
                     purchaseItem(item)
                 });

             let price = item.price.toFixed(2);

             tr.append(
                 $('<td>').text(item.name),
                 $('<td>').text(item.description),
                 $('<td>').text(price),
                 $('<td>').append(purchase)
             )
         }


         function purchaseItem(item) {
             $.ajax({
                 method: 'GET',
                 url: kinveyBaseUrl + "user/" + kinveyAppKey + '/' + sessionStorage.getItem('userId'),
                 headers: getUserAuthHeaders(),
                 success: getUserSuccess,
                 error: handleAjaxError
             });

             function getUserSuccess(user) {
                 addItemToCart(user, item);
             }

             function addItemToCart(user, item) {
                 if(user.cart == undefined) {
                     user.cart = {};
                 }
                 if(user.cart[item._id] == undefined) {
                     user.cart[item._id] = {
                         quantity: {},
                         product: {}
                     };
                     user.cart[item._id].product = item;
                     user.cart[item._id].quantity = 1;
                 } else {
                     user.cart[item._id].quantity++;
                 }


                 $.ajax({
                     method: 'PUT',
                     url: kinveyBaseUrl + "user/" + kinveyAppKey + '/' + sessionStorage.getItem('userId'),
                     data: user,
                     headers: getUserAuthHeaders(),
                     success: successfullyAdded,
                     error: handleAjaxError
                 });

                 function successfullyAdded() {
                     showInfo('Item added to cart');
                     sessionStorage.setItem('cart', JSON.stringify(user.cart));
                 }
             }
         }
     }


    //Cart + discard logic
    function showProductsInCart() {
        $('#cartProducts > table > tbody').empty();

        showView('viewCart');

        let myProducts = JSON.parse(sessionStorage.getItem('cart'));
        console.dir(myProducts);
        let keys = Object.keys(myProducts);

        for(let key of keys) {
            let currentItem =  myProducts[key];
            console.dir(myProducts[key]);
            let tr = $('<tr>');
            addItemToTable(currentItem, tr, key);
            $('#cartProducts > table > tbody').append(tr);
        }


        function addItemToTable(item, tr, key) {
            let price = (item.product.price * item.quantity).toFixed(2);
            console.dir(item);
            let discard = $("<input type='button' value='Discard'>")
                .click( function() {
                    discardItem(key)
                });

            tr.append(
                $('<td>').text(item.product.name),
                $('<td>').text(item.product.description),
                $('<td>').text(item.quantity),
                $('<td>').text(price),
                $('<td>').append(discard)
            )
        }

        function discardItem(productId){
            $.ajax({
                method: 'GET',
                url: kinveyBaseUrl + "user/" + kinveyAppKey + '/' + sessionStorage.getItem('userId'),
                headers: getUserAuthHeaders(),
                success: getUserSuccess,
                error: handleAjaxError
            });

            function getUserSuccess(user) {
                deleteItemFromCart(user, productId);
            }
        }

        function deleteItemFromCart(user, productId) {
            delete user.cart[productId];

            $.ajax({
                method: 'PUT',
                url: kinveyBaseUrl + "user/" + kinveyAppKey + '/' + sessionStorage.getItem('userId'),
                data: user,
                headers: getUserAuthHeaders(),
                success: successfullyDiscarded,
                error: handleAjaxError
            });

            function successfullyDiscarded() {
                if(user.cart != undefined) {
                    sessionStorage.setItem('cart', JSON.stringify(user.cart));
                } else {
                    sessionStorage.setItem('cart', '{}');
                }
                showCartView();
                showInfo('Product discarded.')
            }
        }
    }


    // Views
    function showHomeView() {
        showView("viewAppHome");
    }

    function showLoginView() {
        showView("viewLogin");
        $('#formLogin').trigger('reset');
    }

    function showRegisterView() {
        $("#formRegister").trigger('reset');
        showView('viewRegister');
    }

    function showUserHomeView() {
        $('#viewUserHomeHeading').text('Welcome, ' + sessionStorage.getItem('username') +'!');
        showView('viewUserHome');
    }

    function showShopView() {
        showView('viewShop');
        showAllProducts();
    }

    function showCartView() {
        showProductsInCart();
    }


    // Notifications
    $(document).on({
        ajaxStart: function () {
            $('#loadingBox').show()
        },
        ajaxStop: function () {
            $('#loadingBox').hide()
        }
    });

    $("#infoBox, #errorBox").click(function () {
        $(this).fadeOut();
    });

    function showError(mssg) {
        $('#errorBox').text("Error:" + mssg);
        $('#errorBox').show();
    }

    function showInfo(mssg) {
        $('#infoBox').text(mssg);
        $('#infoBox').show();

        setTimeout(function () {
            $('#infoBox').fadeOut();
        }, 2000)
    }

    function hideNotifications() {
        $('#infoBox, #errorBox, #loadingBox').hide();
    }


    // Utils
    function saveAuthInSession(userInfo) {
        sessionStorage.setItem('username', userInfo.username);
        sessionStorage.setItem('authToken', userInfo._kmd.authtoken);
        sessionStorage.setItem('userId', userInfo._id);
        sessionStorage.setItem('name', userInfo.name);

        if(userInfo.cart != undefined){
            sessionStorage.setItem('cart', JSON.stringify(userInfo.cart));
        } else {
            sessionStorage.setItem('cart', '{}');
        }

        $('#loggedInUser').text('Welcome, ' + userInfo.username);
    }

    function getUserAuthHeaders() {
        return {
            'Authorization': 'Kinvey ' + sessionStorage.getItem('authToken')
        }
    }

    function showView(viewName) {
        //hide all views and show only selected one
        $('main > section').hide();
        $('#' + viewName).show();
    }

    function showHideMenuLinks() {
        $('.useronly, .anonymous').hide();

        if (sessionStorage.getItem('authToken')) {
            $('.useronly').show();
            $('#spanMenuLoggedInUser').text('Welcome, ' + sessionStorage.getItem('username') + '!');
        } else {
            //No User Logged in
            $('.anonymous').show();

        }
    }
    function handleAjaxError(response) {
        let errorMsg = JSON.stringify(response);
        if (response.readyState === 0) {
            errorMsg = "Cannot connect due to network error.";
        }
        if (response.responseJSON && response.responseJSON.description) {
            errorMsg = response.responseJSON.description;
        }
        showError(errorMsg);
    }


}
