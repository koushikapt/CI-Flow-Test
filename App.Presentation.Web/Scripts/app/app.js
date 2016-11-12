var app = function() {
	var a;
	
    function Sum(a, b) {
        /// <summary>Sum two numbers</summary>
        if (typeof a !== "number" || typeof b !== "number")
            throw new Error("Argument must be numbers");

        var sum = (a * 100) + (b * 100);
        if (sum < 0) return 0;
        return Math.round(sum) / 100.0;
    }

    return {
        sum: Sum
    };
}();