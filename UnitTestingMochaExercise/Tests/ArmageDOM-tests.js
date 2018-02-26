let expect = require("chai").expect;
let nuke = require("../ArmageDOM").nuke;


describe("nuke testing", function () {
    beforeEach(
        document.body.innerHTML =
    '<div id="target">' +
        '<div class="nested target">'+
        '<p>This is some text</p>'+
    '</div>'+
    '<div class="target">'+
        '<p>Empty div</p>'+
    '</div>'+
    '<div class="inside">'+
        '<span class="nested">Some more text</span>'+
    '<span class="target">Some more text</span>'+
    '</div>'+
    '</div>'
    );
   describe("with invalid input", function () {
       it("with first argument of different type should do nothing", function () {
           expect(nuke({has: sparta}, 'pesho')).to.equal(null,
               "With invalid arg should be null");
       })
   })
});

