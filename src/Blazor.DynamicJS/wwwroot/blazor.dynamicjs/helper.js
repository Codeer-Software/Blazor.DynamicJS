let objectIdCreate = 0;
let objects = {};

export function invokeMethod(cspRefeenceId, objId, names, theArgs) {

    resolveArgs(theArgs);

    if (names.length == 0 && objId != 0) {
        //functionが入っている場合
        const obj = objects[objId].obj(...theArgs);
        return setObject(cspRefeenceId, obj, cspRefeenceId);
    }
    else {
        const info = getInvokeInfo(objId, names);
        const obj = info.target == null ?
            window[info.last].apply(info.target, theArgs) :
            info.target[info.last].apply(info.target, theArgs);
        return setObject(cspRefeenceId, obj, cspRefeenceId);
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

export function getIndex(cspRefeenceId, objId, names, index) {
    if (names.length == 0) return objects[objId].obj;
    const info = getInvokeInfo(objId, names);
    var obj = (info.target == null) ? window[info.last][index] : info.target[info.last][index];
    return setObject(cspRefeenceId, obj, cspRefeenceId);
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
    if (names.length == 0) return objects[objId].obj;
    const info = getInvokeInfo(objId, names);
    return (info.target == null) ? window[info.last] : info.target[info.last];
}

export function setObject(cspRefeenceId, obj) {
    const objId = ++objectIdCreate;
    objects[objId] = { obj, cspRefeenceId };
    return objId;
}

export async function importModule(cspRefeenceId, path) {
    const mod = await import(path);
    const id = setObject(cspRefeenceId, mod);
    return new Promise(resolve => {
        resolve(id);
    }); 
}

export function createObject(cspRefeenceId, names, theArgs) {

    resolveArgs(theArgs);

    const info = getInvokeInfo(0, names);

    const c = info.target == null ? window[info.last] : info.target[info.last];
    const obj = new c(...theArgs);

    return setObject(cspRefeenceId, obj, cspRefeenceId);
}

export function createFunction(cspRefeenceId, objRef, method, dynamicIndexes) {
    var func = (...theArgs) => {

        var newArgs = [];
        for (let i = 0; i < theArgs.length; i++) {
            if (dynamicIndexes.includes(i)) {
                newArgs.push(setObject(cspRefeenceId, theArgs[i]));
            } else {
                newArgs.push(theArgs[i]);
            }
        }

        var ret = objRef.invokeMethod(method, ...newArgs);
        if (ret && ret.hasOwnProperty("blazorDynamicJavaScriptObjectId")) {
            var objId = ret["blazorDynamicJavaScriptObjectId"];
            var names = ret["blazorDynamicJavaScriptUnresolvedNames"];
            if (names.length == 0) {
                return objects[objId].obj;
            } else {
                const info = getInvokeInfo(objId, names);
                const obj = (info.target == null) ? window[info.last] : info.target[info.last];
                return obj;
            }
        }
        return ret;
    }
    return setObject(cspRefeenceId, func);
}

export function dispose(cspRefeenceId) {
    for (let key in objects) {
        if (objects[key].cspRefeenceId === cspRefeenceId) {
            delete objects[key];
        }
    }
}

function resolveArgs (theArgs) {
    for (let i = 0; i < theArgs.length; i++) {
        if (theArgs[i].hasOwnProperty("blazorDynamicJavaScriptObjectId")) {
            var objId = theArgs[i]["blazorDynamicJavaScriptObjectId"];
            var names = theArgs[i]["blazorDynamicJavaScriptUnresolvedNames"];

            if (names.length == 0) {
                theArgs[i] = objects[objId].obj;
            } else {
                const info = getInvokeInfo(objId, names);
                const obj = (info.target == null) ? window[info.last] : info.target[info.last];
                theArgs[i] = obj;
            }
        }
    }
}

function getInvokeInfo (objId, names) {
    const last = names.slice(-1)[0];
    const others = names.slice(0, -1);
    let target = null;
    if (objId != 0) {
        target = objects[objId].obj;
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
