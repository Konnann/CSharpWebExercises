let expect = require('chai').expect;
let createList = require('../ListCLass').createList;

describe('test createList() funtionality', function () {
    let list = {};
    beforeEach(function () {
        list = createList();
    });
    //no tests
    it('check for requred properties', function () {
        expect(typeof list.add).to.equal('function',
            'add function does not exist');
        expect(typeof list.shiftLeft).to.equal('function',
            'shiftLeft function does not exist');
        expect(typeof list.shiftRight).to.equal('function',
            'shiftRight function does not exist');
        expect(typeof list.swap).to.equal('function',
            'swap function does not exist');
        expect(typeof list.toString).to.equal('function',
            'toString function does not exist');
    });

    // no tests
    it('should start with empty list', function () {
        expect(list.toString()).to.equal('',
            'List was not initially empty');
        list.shiftLeft();
        expect(list.toString()).to.equal('',"Error");
        list.shiftRight();
        expect(list.toString()).to.equal('', 'Error');
        expect(list.swap(0, 1)).to.equal(false, 'Error');
        expect(list.toString()).to.equal('', 'Error' );
    });
    //no tests
    it('test add function with one add', function () {
        list.add(5);
        expect(list.toString()).to.equal('5', 'Error');
    });
    //no tests
    it('test add function order', function(){
        list.add(3);
        list.add('kozichka');
        list.add({name: 'pesho'});
        expect(list.toString()).to.equal('3, kozichka, [object Object]','Error');
        list.add();
        expect(list.toString()).to.equal('3, kozichka, [object Object], ', 'Error');
    });
    //2 tests
    it('test shiftLeft function', function () {
        list.add(3);
        list.add("Pesho");
        list.add(45);
        list.add(1);
        list.shiftLeft();
        expect(list.toString()).to.equal('Pesho, 45, 1, 3',
            'Shift Left function does not work properly');
    });
    //no tests
    it('test shiftLeft with one list item', function(){
        list.add('awesome');
        list.shiftLeft();
        expect(list.toString()).to.equal('awesome', 'Error');
    });
    //2 tests
    it('test shiftRight function', function () {
        list.add(3);
        list.add("Pesho");
        list.add(45);
        list.add(1);
        list.shiftRight();
        expect(list.toString()).to.equal('1, 3, Pesho, 45',
            'Shift Right function does not work properly');
    });
    it('test shiftRight function with one list element', function () {
        list.add('snek');
        list.shiftRight();
        expect(list.toString()).to.equal('snek', 'Error');
    });

// 3 tests
    describe('test swap function with correct arguments', function () {
        it('should swap elements with valid indexes', function () {
            list.add(3);
            list.add("Pesho");
            list.add(45);
            list.add(1);
            expect(list.swap(1, 3)).to.equal(true, 'Error');
            expect(list.toString()).to.equal('3, 1, 45, Pesho',
                'Swap function does not work properly');
        });

        it('swap should return true if successful', function () {
            list.add(3);
            list.add("Pesho");
            list.add([45, 22]);
            list.add(1);
            expect(list.swap(3, 0)).to.equal(true,
                'Swap should return true if successful');
            expect(list.toString()).to.equal('1, Pesho, 45,22, 3', 'Error');
        });
    });
    describe('test swap function with incorrect arguments', function(){
        //1 test
        it('should not change list if first or second index is outside boundaries', function(){
            list.add(3);
            list.add("Pesho");
            list.add(45);
            list.add(1);
            expect(list.swap(1, 4)).to.equal(false, 'Error');
            expect(list.toString()).to.equal('3, Pesho, 45, 1',
                'Swap should not change list given incorrect indexes');

            expect(list.swap(-1, 3)).to.equal(false, 'Error');
            expect(list.toString()).to.equal('3, Pesho, 45, 1',
                'Swap should not change list given incorrect indexes');

            expect(list.swap(0, 6)).to.equal(false, 'Error');
            expect(list.toString()).to.equal('3, Pesho, 45, 1',
                'Swap should not change list given incorrect indexes');

            expect(list.swap(-2, 2)).to.equal(false,
                'Swap should return false if unsuccessful');
            expect(list.toString()).to.equal('3, Pesho, 45, 1',
                'Swap should not change list given incorrect indexes');
        });
        //3 tests
        it('should not change list when given floating point number', function () {
            list.add(3);
            list.add("Pesho");
            list.add(45);
            list.add(1);
            expect(list.swap(2.4, 3)).to.equal(false,
                'Swap should return false if unsuccessful');
            expect(list.toString()).to.equal('3, Pesho, 45, 1',
                'Swap should not change list given incorrect indexes');
            expect(list.swap(0, 1.78)).to.equal(false, "Error");
            expect(list.toString()).to.equal('3, Pesho, 45, 1',
                'Swap should not change list given incorrect indexes');
            expect(list.swap(2.4, 1.78)).to.equal(false, 'Error');
            expect(list.toString()).to.equal('3, Pesho, 45, 1',
                'Swap should not change list given incorrect indexes');
        });

        //1 test
        it('should not change list if indexes are equal', function(){
            list.add(3);
            list.add("Gosho");
            list.add(45);
            list.add([2]);
            expect(list.swap(2, 2)).to.equal(false, 'Error');
            expect(list.toString()).to.equal('3, Gosho, 45, 2',
                'Swap should not change list when given equal indexes');
        });

        it('should return false if swapping is unsuccessful', function () {
            list.add(3);
            list.add("Pesho");
            list.add(45);
            list.add(1);
            expect(list.swap(-1, 4)).to.equal(false,
                'Swap should return false when unsuccessful');
            expect(list.toString()).to.equal('3, Pesho, 45, 1');
            expect(list.swap(0, 4)).to.equal(false,
                'Swap should return false when unsuccessful');
            expect(list.toString()).to.equal('3, Pesho, 45, 1');
        });

        it('should return false if given equal floating point numbers', function(){
            list.add(4);
            list.add(22);
            expect(list.swap(2,5, 2.5)).to.equal(false, 'Error');
            expect(list.toString()).to.equal('4, 22');
        });

        it('should do nothing if there is only one element in the list', function () {
            list.add('pomosht');
            expect(list.swap(0, 2)).to.equal(false, 'Error');
            expect(list.toString()).to.equal('pomosht', 'Error');
        });
        it('should do nothing with one empty index', function () {
            list.add('92 is not enough');
            expect(list.swap(0)).to.equal(false, 'Error');
            expect(list.toString()).to.equal('92 is not enough', 'Error');
        });

    });
});
    
