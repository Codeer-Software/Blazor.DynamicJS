let objectIdCreate = 0;
let objects = {};

export function invokeMethod (objId, names, theArgs) {

    resolveArgs(theArgs);

    if (names.length == 0 && objId != 0) {
        //functionが入っている場合
        const obj = objects[objId](...theArgs);
        return setObject(obj);
    }
    else {
        const info = getInvokeInfo(objId, names);
        const obj = info.target == null ?
            window[info.last].apply(info.target, theArgs) :
            info.target[info.last].apply(info.target, theArgs);
        return setObject(obj);
    }
}

export function setProperty (objId, names, obj) {
    var vals = [obj];
    resolveArgs(vals);
    obj = vals[0];


    const info = getInvokeInfo(objId, names);

    if (info.target === null) window[info.last] = obj;
    else info.target[info.last] = obj;
}

export function getIndex (objId, names, index) {
    if (names.length == 0) return objects[objId];
    const info = getInvokeInfo(objId, names);
    var obj = (info.target == null) ? window[info.last][index] : info.target[info.last][index];
    return setObject(obj);
}

export function setIndex (objId, names, index, obj) {
    var vals = [obj];
    resolveArgs(vals);
    obj = vals[0];


    const info = getInvokeInfo(objId, names);

    if (info.target === null) window[info.last][index] = obj;
    else info.target[info.last][index] = obj;
}

export function getObject (objId, names) {
    if (names.length == 0) return objects[objId];
    const info = getInvokeInfo(objId, names);
    return (info.target == null) ? window[info.last] : info.target[info.last];
}

export function setObject (obj) {
    const objId = ++objectIdCreate;
    objects[objId] = obj;
    return objId;
}

export function createObject (names, theArgs) {

    resolveArgs(theArgs);

    const info = getInvokeInfo(0, names);

    const c = info.target == null ? window[info.last] : info.target[info.last];
    const obj = new c(...theArgs);

    return setObject(obj);
}

export function createFunction (objRef, method) {
    var func = (...theArgs) => {
        return objRef.invokeMethod(method, ...theArgs);
    }
    return setObject(func);
}

export function resolveArgs (theArgs) {
    for (let i = 0; i < theArgs.length; i++) {
        if (theArgs[i].hasOwnProperty("blazorDynamicJavaScriptObjectId")) {
            var objId = theArgs[i]["blazorDynamicJavaScriptObjectId"];
            var names = theArgs[i]["blazorDynamicJavaScriptUnresolvedNames"];

            if (names.length == 0) {
                theArgs[i] = objects[objId];
            } else {
                const info = getInvokeInfo(objId, names);
                const obj = (info.target == null) ? window[info.last] : info.target[info.last];
                theArgs[i] = obj;
            }
        }
    }
}

export function getInvokeInfo (objId, names) {
    const last = names.slice(-1)[0];
    const others = names.slice(0, -1);
    let target = null;
    if (objId != 0) {
        target = objects[objId];
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
