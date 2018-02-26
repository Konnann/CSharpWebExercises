/**
 * Created by Dannie on 11/1/2016.
 */
function isOddOrEven(string) {
    if (typeof(string) !== 'string') {
        return undefined;
    }
    if (string.length % 2 === 0) {
        return "even";
    }

    return "odd";
}
module.exports = { isOddOrEven };
