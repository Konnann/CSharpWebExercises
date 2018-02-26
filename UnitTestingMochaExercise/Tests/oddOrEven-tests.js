/**
 * Created by Dannie on 11/1/2016.
 */

let expect = require("chai").expect;
let isOddOrEven = require('../oddOrEven').isOddOrEven;

describe("isOddOrEven", function() {
    it("with a number parameter should return undefined", function () {
        expect(isOddOrEven(13)).to.equal(undefined,
            "Function did not return the correct result!");
    });
    it("with object parameter should return undefined", function () {
        expect(isOddOrEven({name: "pesho"})).to.equal(undefined,
            "Function did not return the correct result!");
    });
    it("with an even length string, should return correct result", function () {
        expect(isOddOrEven("roar")).to.equal("even",
            "Function did not return the correct result!");
    });
    it("with multiple consequitive checks, should return correct values", function () {
        expect(isOddOrEven("cat")).to.equal("odd",
            "Function did not return the correct result!");
        expect(isOddOrEven("alabala")).to.equal("odd",
            "Function did not return the correct result!");
        expect(isOddOrEven("is it even")).to.equal("even",
            "Function did not return the correct result!");
    });

});