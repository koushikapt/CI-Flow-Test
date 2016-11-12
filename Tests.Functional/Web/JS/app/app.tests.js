/// <reference path="../tests.js" chutzpah-exclude="true" />
/// <reference path="../../../../App.Presentation.Web/Scripts/app/app.js"  />

describe("App Object", function () {
    it("should be defined", function () {
        expect(app).toBeDefined();
    });

    it("should sum up to 2 digits after point", function () {
        expect(app.sum(0.1, 0.2)).toBe(0.3);
        expect(app.sum(1002, 1000.2)).toBe(2002.2);
        expect(app.sum(123.456, 654.111)).toBe(777.57);
        expect(app.sum(-123.45, -22.2)).toBe(0);
        expect(app.sum(-123.45, 22.2)).toBe(0);
        expect(app.sum(123.45, -22.2)).toBe(101.25);
        expect(function () { app.sum("123.45", -22.2); }).toThrowError("Argument must be numbers");
    });
});