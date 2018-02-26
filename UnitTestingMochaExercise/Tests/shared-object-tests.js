/**
 * Created by Dannie on 11/2/2016.
 */
let expect = require("chai").expect;
let jsdom = require('jsdom-global')();
let $ = require('jquery');
let sharedObject = require("../shared-object").sharedObject;


describe("sharedObject", function () {
    let sharedObj,
        html;

    beforeEach(() => {
        sharedObj = Object.create(sharedObject);
        document.body.innerHTML =
            '<div id="wrapper">' +
            '<input type="text" id="name">' +
            '<input type="text" id="income">' +
            '</div>';
    });

    describe("initialValues", function () {
        it("default value of name should be null", function () {
            expect(sharedObj.name).to.equal(null,
                "Name default value is not null");
        });
        it("default value of income should be null", function () {
            expect(sharedObj.income).to.equal(null,
                "Income default value is not null");
        });
    });

    describe("changeName", function () {
        it('with empty string should not make changes', function () {
            sharedObj.changeName("");
            expect(sharedObj.name).to.equal(null,
                "Function returned wrong result");
        });
        it('with wrong data type should not make changes', function () {
            sharedObj.changeName(54);
            expect(sharedObj.name).to.equal(null,
                "Function returned wrong result");
        });
        it("with empty string should not make changes", function () {
            sharedObj.changeName("Stamat");
            sharedObj.changeName("");
            expect(sharedObj.name).to.equal("Stamat",
                "Function returned wrong result");
            expect($('#name').val()).to.equal("Stamat",
                "Function returned wrong result");
        });
        it("with name, should return correct result", function () {
            let name = "Angel";
            sharedObj.changeName(name);
            expect(sharedObj.name).to.equal(name,
                "Function returned wrong result");
            expect($('#name').val()).to.equal(name,
                "Function returned wrong result");
        });
    });
    
    describe("changeIncome",function () {
        it('with negative int should not make changes', function () {
            sharedObj.changeIncome(-20);
            expect(sharedObj.name).to.equal(null,
                "Function returned wrong result");
        });
        it('with not correct value type should not make changes', function () {
            sharedObj.changeIncome('str');
            expect(sharedObj.income).to.equal(null,
                "Function returned wrong result");
        });
        it("with 0 should not make changes", ()=> {
            sharedObj.changeIncome(0);
            expect(sharedObj.income).to.equal(null);
            $('#income').val(sharedObj.income);
        });
        it("with negative number should not make changes", function () {
            sharedObj.changeIncome(10);
            sharedObj.changeIncome(-10);
            expect(sharedObj.income).to.equal(10,
                "Function returned wrong result");
        });
        it("with positive integer should return correct result", function () {
            sharedObj.changeIncome(400);
            expect(sharedObj.income).to.equal(400,
                "Function returned wrong result");
            expect($('#income').val()).to.equal('400',
                "Function returned wrong result");
        })
    });
    
    describe("updateName", function () {
        it('with wrong data type should not make changes', function () {
            $('#name').val(54);
            sharedObj.updateName();
            expect(sharedObj.name).to.equal(null,
                "Function returned wrong result");
        });
        it('with empty string should not make changes', function () {
            $('#name').val("");
            sharedObj.updateName();
            expect(sharedObj.name).to.equal(null,
                "Function returned wrong result");
        });
        it("with empty string in textbox should not make changes", function () {
            $('#name').val("Nasko");
            sharedObj.updateName();
            $('#name').val("");
            sharedObj.updateName();
            expect(sharedObj.name).to.equal('Nasko',
                "Function returned wrong result");
        });

        it("with string in textbox should set value of name", function () {
            $('#name').val("Angel");
            sharedObj.updateName();
            expect(sharedObj.name).to.equal("Angel",
                "Function returned wrong result");
        })
    });
    
    describe("updateIncome", function () {
        it("with empty value, shoule not make changes", function () {
            sharedObj.updateIncome();
            expect(sharedObj.income).to.equal(null,
                "Function returned wrong result");

        });
        it('with negative int should not make changes', function () {
            $('#income').val(20);
            sharedObj.updateIncome();
            expect(sharedObj.income).to.equal(null,
                "Function returned wrong result");
        });
        it("with negative integer should not make changes", function () {
            $('#income').val(20);
            sharedObj.updateIncome();
            $('#income').val(-5);
            sharedObj.updateIncome();
            expect(sharedObj.income).to.equal(20,
                "Function returned wrong result");
        });
        it("with floating point number should not make changes", function () {
            $('#income').val(20);
            sharedObj.updateIncome();
            $('#income').val(3.5);
            sharedObj.updateIncome();
            expect(sharedObj.income).to.equal(20,
                "Function returned wrong result");
        });
        it("with positive integer should return correct value", function () {
            $('#income').val(30);
            sharedObj.updateIncome();
            expect(sharedObj.income).to.equal(30,
                "Function returned wrong result");
        });
        it("with string should not make changes", function () {
            $('#income').val("Kozichka");
            sharedObj.updateIncome();
            expect(sharedObj.income).to.equal(null,
                "Function returned wrong result");
        });
    });
});