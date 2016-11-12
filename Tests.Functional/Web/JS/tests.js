// Mock the bind
Function.prototype.bind = function (obj) {
    var oldFunction = this;
    oldFunction.binded = function () {
        oldFunction.apply(obj, arguments);
    };
    return oldFunction.binded;
};

// Create helper
var tests = function () {
    var requests = {}, element;

    function FixturesInit() {
        element = document.createElement("div");
        element.className = "test-content";
        document.body.appendChild(element);
    }

    function AjaxInit() {
        if (window.XMLHttpRequest) {
            function MockXmlHttpRequest() {
                this.open = function (method, url, async) {
                    this.type = method;
                    this.url = url;
                    this.req = {
                        request: this,
                        resolve: function (data) {
                            this.request.readyState = 4;
                            this.request.status = 200;
                            this.request.responseText = data;
                            try {
                                this.request.responseXML = $.parseXML(data);
                            }
                            catch (e) { }
                            if (this.request.onload) {
                                this.request.onload();
                            }
                            else if (this.request.onreadystatechange) {
                                this.request.onreadystatechange();
                            }
                        }
                    };
                };

                this.send = function (data) {
                    this.data = data;
                    if (typeof requests[url] === "undefined") {
                        requests[url] = [];
                    }

                    requests[this.url].push(this.req);
                };

                this.abort = function () {
                    console.log("ABORT");
                }

                this.setRequestHeader = function () { };

                this.getAllResponseHeaders = function () { return ""; };
            }

            window.XMLHttpRequest = MockXmlHttpRequest;
        }
    }

    (function Init() {
        document.addEventListener("DOMContentLoaded", function () {
            document.removeEventListener("DOMContentLoaded", arguments.callee, false);
            FixturesInit();
            AjaxInit();
        }, false);
    }());

    return {
        requests: requests,
        clear: function () {
            element.innerHTML = "";
            requests = {};
            location.hash = "";
        }
    };
}();