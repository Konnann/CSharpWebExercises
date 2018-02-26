/**
 * Created by Dannie on 11/2/2016.
 */
let expect = require("chai").expect;
let mathEnforcer = require("../MathEnforcer").mathEnforcer;

describe("mathEnforcer", function () {
    describe('addFive', function () {
        it("with non-number parameter, should return undefined", function () {
            expect(mathEnforcer.addFive("gosho")).to.equal(undefined ,
                "The function returned an incorrect answer. Should equal undefined");
        });
        it("with positive number should return correct result", function () {
            expect(mathEnforcer.addFive(55)).to.equal(60,
                "The function returned an incorrect answer.");
        });
        it("with negative number should return correct result", function () {
           expect(mathEnforcer.addFive(-20)).to.equal(-15,
               "The function returned an incorrect answer.");
        });
        it("with floating point number, should return correct result", function () {
            expect(mathEnforcer.addFive(10.15)).to.be.closeTo(15.15, 0.01,
                "The function returned an incorrect answer.");
        });
    });
    
    describe('substractTen', function () {
        it("with non-number parameter, should return undefined", function () {
            expect(mathEnforcer.subtractTen("gosho")).to.equal(undefined ,
                "The function returned an incorrect answer. Should equal undefined");
        });
        it("with positive number should return correct result", function () {
            expect(mathEnforcer.subtractTen(55)).to.equal(45,
                "The function returned an incorrect answer.");
        });
        it("with negative number should return correct result", function () {
            expect(mathEnforcer.subtractTen(-20)).to.equal(-30,
                "The function returned an incorrect answer.");
        });
        it("with floating point number, should return correct result", function () {
            expect(mathEnforcer.subtractTen(10.15)).to.be.closeTo(0.15, 0.01,
                "The function returned an incorrect answer.");
        });    });
    
    describe('sum', function () {
        it("with first non-number parameter, should return undefined", function () {
            expect(mathEnforcer.sum("gosho", 4)).to.equal(undefined ,
                "The function returned an incorrect answer. Should equal undefined");
        });
        it("with second non-number parameter, should return undefined", function () {
            expect(mathEnforcer.sum(25, "gosho")).to.equal(undefined ,
                "The function returned an incorrect answer. Should equal undefined");
        });
        it("with positive numbers should return correct result", function () {
            expect(mathEnforcer.sum(55, 20)).to.equal(75,
                "The function returned an incorrect answer.");
        });
        it("with negative number should return correct result", function () {
            expect(mathEnforcer.sum(-20, -5)).to.equal(-25,
                "The function returned an incorrect answer.");
        });
        it("with floating point number, should return correct result", function () {
            expect(mathEnforcer.sum(10.6, 5.1)).to.be.closeTo(15.7, 0.01,
                "The function returned an incorrect answer.");
        });
    })
});

