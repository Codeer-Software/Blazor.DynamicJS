window.BlazorDynamicJavaScriptHelper =
{
    objectIdCreate: 0,
    objects: {},

    invokeMethod: function (objId, names, theArgs) {

        window.BlazorDynamicJavaScriptHelper.resolveArgs(theArgs);

        if (names.length == 0 && objId != 0) {
            //functionが入っている場合
            const obj = window.BlazorDynamicJavaScriptHelper.objects[objId](...theArgs);
            return window.BlazorDynamicJavaScriptHelper.setObject(obj);
        }
        else {
            const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(objId, names);
            const obj = info.target == null ?
                window[info.last].apply(info.target, theArgs) :
                info.target[info.last].apply(info.target, theArgs);
            return window.BlazorDynamicJavaScriptHelper.setObject(obj);
        }
    },

    setProperty: function (objId, names, obj) {
        var vals = [obj];
        window.BlazorDynamicJavaScriptHelper.resolveArgs(vals);
        obj = vals[0];


        const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(objId, names);

        if (info.target === null) window[info.last] = obj;
        else info.target[info.last] = obj;
    },

    getIndex: function (objId, names, index) {
        if (names.length == 0) return window.BlazorDynamicJavaScriptHelper.objects[objId];
        const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(objId, names);
        var obj = (info.target == null) ? window[info.last][index] : info.target[info.last][index];
        return window.BlazorDynamicJavaScriptHelper.setObject(obj);
    },

    setIndex: function (objId, names, index, obj) {
        var vals = [obj];
        window.BlazorDynamicJavaScriptHelper.resolveArgs(vals);
        obj = vals[0];


        const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(objId, names);

        if (info.target === null) window[info.last][index] = obj;
        else info.target[info.last][index] = obj;
    },

    getObject: function (objId, names) {
        if (names.length == 0) return window.BlazorDynamicJavaScriptHelper.objects[objId];
        const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(objId, names);
        return (info.target == null) ? window[info.last] : info.target[info.last];
    },

    setObject: function (obj) {
        const objId = ++window.BlazorDynamicJavaScriptHelper.objectIdCreate;
        window.BlazorDynamicJavaScriptHelper.objects[objId] = obj;
        return objId;
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
                var objId = theArgs[i]["blazorDynamicJavaScriptObjectId"];
                var names = theArgs[i]["blazorDynamicJavaScriptUnresolvedNames"];

                if (names.length == 0) {
                    theArgs[i] = window.BlazorDynamicJavaScriptHelper.objects[objId];
                } else {
                    const info = window.BlazorDynamicJavaScriptHelper.getInvokeInfo(objId, names);
                    const obj = (info.target == null) ? window[info.last] : info.target[info.last];
                    theArgs[i] = obj;
                }
            }
        }
    },

    getInvokeInfo: function (objId, names) {
        const last = names.slice(-1)[0];
        const others = names.slice(0, -1);
        let target = null;
        if (objId != 0) {
            target = window.BlazorDynamicJavaScriptHelper.objects[objId];
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
