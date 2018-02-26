/**
 * Created by Dannie on 11/1/2016.
 */
let expect = require("chai").expect;
let lookupChar = require("../charLookup").lookupChar;

describe('lookupChar', function () {
   it("with a non-string first parameter, should return undefined", function () {
       expect(lookupChar(13,0)).to.equal(undefined,
            "The function did not return the correct result!");
   });
    it("with a non-number second parameter, should return undefined", function () {
        expect(lookupChar("pesho", "gosho")).to.equal(undefined,
            "The function did not return the correct result!")
    });
    it("with a floating point number second parameter, should return undefined", function () {
        expect(lookupChar("pesho", 3.12)).to.equal(undefined,
            "The function did not return the correct result!");
    });
    it("with an incorrect index value, should return incorrect index", function () {
        expect(lookupChar("stamat", 13)).to.equal("Incorrect index",
            "The function did not return the correct result!");
    });
    it("with a negative index value, should return incorrect index", function () {
        expect(lookupChar("gosho", -3)).to.equal("Incorrect index",
            "The function did not return the correct result!");
    });
    it("with an index value equal to the string length, should return incorrect index", function () {
        expect(lookupChar("pesho", 5)).to.equal("Incorrect index",
            "The function did not return the correct result!");
    });
    it("with correct parameters, should return correct values", function () {
        expect(lookupChar("pesho", 0)).to.equal("p",
            "The function did not return the correct result!");
    });
    it("with correct parameters, should return correct values", function () {
        expect(lookupChar("stamat", 3)).to.equal("m",
            "The function did not return the correct result!");
    });

});