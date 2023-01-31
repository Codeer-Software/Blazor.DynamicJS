window.BlazorDynamicJavaScriptHelper =
{
    index: 0,
    objects: {},

    invokeMethod: function (id, names, theArgs) {

        window.BlazorDynamicJavaScriptHelper.resolveArgs(theArgs);

        if (names.length == 0 && id != 0) {
            //functionが入っている場合
            const obj = window.BlazorDynamicJavaScriptHelper.objects[id](...theArgs);
            return window.BlazorDynamicJavaScriptHelper.setObject(obj);
        }
        else {
            const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(id, names);
            const obj = info.target == null ?
                window[info.last].apply(info.target, theArgs) :
                info.target[info.last].apply(info.target, theArgs);
            return window.BlazorDynamicJavaScriptHelper.setObject(obj);
        }
    },

    setProperty: function (id, names, obj) {
        var vals = [obj];
        window.BlazorDynamicJavaScriptHelper.resolveArgs(vals);
        obj = vals[0];


        const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(id, names);

        if (info.target === null) window[info.last] = obj;
        else info.target[info.last] = obj;
    },

    getObject: function (id, names) {
        if (names.length == 0) return window.BlazorDynamicJavaScriptHelper.objects[id];
        const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(id, names);
        return (info.target == null) ? window[info.last] : info.target[info.last];
    },

    setObject: function (obj) {
        const id = ++window.BlazorDynamicJavaScriptHelper.index;
        window.BlazorDynamicJavaScriptHelper.objects[id] = obj;
        return id;
    },

    createObject: function (names, theArgs) {

        window.BlazorDynamicJavaScriptHelper.resolveArgs(theArgs);

        const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(0, names);

        const c = info.target == null ? window[info.last] : info.target[info.last];
        const obj = new c(...theArgs);

        return window.BlazorDynamicJavaScriptHelper.setObject(obj);
    },

    createFunction: function (objRef, method) {
        var func = (...theArgs) => {
            return objRef.invokeMethod(method, ...theArgs);
        }
        return window.BlazorDynamicJavaScriptHelper.setObject(func);
    },

    resolveArgs: function (theArgs) {
        for (let i = 0; i < theArgs.length; i++) {
            if (theArgs[i].hasOwnProperty("blazorDynamicJavaScriptObjectId")) {
                var id = theArgs[i]["blazorDynamicJavaScriptObjectId"];
                var names = theArgs[i]["blazorDynamicJavaScriptUnresolvedNames"];

                if (names.length == 0) {
                    theArgs[i] = window.BlazorDynamicJavaScriptHelper.objects[id];
                } else {
                    const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(id, names);
                    const obj = (info.target == null) ? window[info.last] : info.target[info.last];
                    theArgs[i] = obj;
                }
            }
        }
    },

    getInvokeInfo: function (id, names) {
        const last = names.slice(-1)[0];
        const others = names.slice(0, -1);
        let target = null;
        if (id != 0) {
            target = window.BlazorDynamicJavaScriptHelper.objects[id];
        }
        for (let e of others) {
            if (target == null) target = window[e];
            else target = target[e];
        }
        var ret = {
            last,
            target
        };
        return ret;
    }
}
