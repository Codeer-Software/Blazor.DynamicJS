let objectIdCreate = 0;
let objects = {};

export function invokeMethod(cspRefeenceId, objId, names, theArgs) {

    resolveArgs(theArgs);

    if (names.length == 0 && objId != 0) {
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

export function invokeMethodAndGetObject(cspRefeenceId, objId, names, theArgs) {
    let retObjId = invokeMethod(cspRefeenceId, objId, names, theArgs);
    return objects[retObjId].obj;
}

export function isEqual(objId, names, obj) {
    const vals = [obj];
    resolveArgs(vals);
    obj = vals[0];

    const info = getInvokeInfo(objId, names);

    let target;
    if (info.target === null) target = window[info.last];
    else {
        if (info.last) return target = info.target[info.last];
        else target = info.target;
    }
    var x = target === obj;
    return x;
}

export function isTrue(objId, names) {
    const info = getInvokeInfo(objId, names);

    let target;
    if (info.target === null) target = window[info.last];
    else {
        if (info.last) target = info.target[info.last];
        else target = info.target;
    }
    if (target) return true;
    return false;
}

export function setProperty (objId, names, obj) {
    const vals = [obj];
    resolveArgs(vals);
    obj = vals[0];

    const info = getInvokeInfo(objId, names);

    if (info.target === null) window[info.last] = obj;
    else info.target[info.last] = obj;
}

export function getIndex(cspRefeenceId, objId, names, index) {
    let obj;
    if (names.length == 0) {
        obj = objects[objId].obj[index];
    } else {
        const info = getInvokeInfo(objId, names);
        obj = (info.target == null) ? window[info.last][index] : info.target[info.last][index];
    }
    return setObject(cspRefeenceId, obj);
}

export function setIndex (objId, names, index, obj) {
    const vals = [obj];
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

export function createObject(cspRefeenceId, objId, names, theArgs) {

    resolveArgs(theArgs);

    const info = getInvokeInfo(objId, names);

    const c = info.target == null ? window[info.last] : info.target[info.last];
    const obj = new c(...theArgs);

    return setObject(cspRefeenceId, obj, cspRefeenceId);
}

export function createFunction(cspRefeenceId, objRef, method, argsCount, dynamicIndexes) {
    const core = (...theArgs) => {

        const newArgs = [];
        for (let i = 0; i < theArgs.length; i++) {
            if (dynamicIndexes.includes(i)) {
                newArgs.push(setObject(cspRefeenceId, theArgs[i]));
            } else {
                newArgs.push(theArgs[i]);
            }
        }

        const ret = objRef.invokeMethod(method, ...newArgs);
        if (ret && ret.hasOwnProperty("blazorDynamicJavaScriptObjectId")) {
            const objId = ret["blazorDynamicJavaScriptObjectId"];
            const names = ret["blazorDynamicJavaScriptUnresolvedNames"];
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

    let func = core;
    switch (argsCount) {
        case 0: func = () => core(); break;
        case 1: func = (a1) => core(a1); break;
        case 2: func = (a1, a2) => core(a1, a2); break;
        case 3: func = (a1, a2, a3) => core(a1, a2, a3); break;
        case 4: func = (a1, a2, a3, a4) => core(a1, a2, a3, a4); break;
        case 5: func = (a1, a2, a3, a4, a5) => core(a1, a2, a3, a4, a5); break;
        case 6: func = (a1, a2, a3, a4, a5, a6) => core(a1, a2, a3, a4, a5, a6); break;
        case 7: func = (a1, a2, a3, a4, a5, a6, a7) => core(a1, a2, a3, a4, a5, a6, a7); break;
        case 8: func = (a1, a2, a3, a4, a5, a6, a7, a8) => core(a1, a2, a3, a4, a5, a6, a7, a8); break;
        case 9: func = (a1, a2, a3, a4, a5, a6, a7, a8, a9) => core(a1, a2, a3, a4, a5, a6, a7, a8, a9); break;
        case 10: func = (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10) => core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10); break;
        case 11: func = (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11) => core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11); break;
        case 12: func = (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12) => core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12); break;
        case 13: func = (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13) => core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13); break;
        case 14: func = (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14) => core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14); break;
        case 15: func = (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15) => core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15); break;
        case 16: func = (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16) => core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16); break;
    }

    return setObject(cspRefeenceId, func);
}



export function createAsyncFunction(cspRefeenceId, objRef, method, argsCount, dynamicIndexes) {
    const core = async (...theArgs) => {

        const newArgs = [];
        for (let i = 0; i < theArgs.length; i++) {
            if (dynamicIndexes.includes(i)) {
                newArgs.push(setObject(cspRefeenceId, theArgs[i]));
            } else {
                newArgs.push(theArgs[i]);
            }
        }

        const ret = await objRef.invokeMethodAsync(method, ...newArgs);
        if (ret && ret.hasOwnProperty("blazorDynamicJavaScriptObjectId")) {
            const objId = ret["blazorDynamicJavaScriptObjectId"];
            const names = ret["blazorDynamicJavaScriptUnresolvedNames"];
            if (names.length == 0) {
                return new Promise(resolve => {
                    resolve(objects[objId].obj);
                });
            } else {
                const info = getInvokeInfo(objId, names);
                const obj = (info.target == null) ? window[info.last] : info.target[info.last];
                return new Promise(resolve => {
                    resolve(obj);
                });
            }
        }
        return new Promise(resolve => {
            resolve(ret);
        });
    }

    let func = core;
    switch (argsCount) {
        case 0: func = async () => await core(); break;
        case 1: func = async (a1) => await core(a1); break;
        case 2: func = async (a1, a2) => await core(a1, a2); break;
        case 3: func = async (a1, a2, a3) => await core(a1, a2, a3); break;
        case 4: func = async (a1, a2, a3, a4) => await core(a1, a2, a3, a4); break;
        case 5: func = async (a1, a2, a3, a4, a5) => await core(a1, a2, a3, a4, a5); break;
        case 6: func = async (a1, a2, a3, a4, a5, a6) => await core(a1, a2, a3, a4, a5, a6); break;
        case 7: func = async (a1, a2, a3, a4, a5, a6, a7) => await core(a1, a2, a3, a4, a5, a6, a7); break;
        case 8: func = async (a1, a2, a3, a4, a5, a6, a7, a8) => await core(a1, a2, a3, a4, a5, a6, a7, a8); break;
        case 9: func = async (a1, a2, a3, a4, a5, a6, a7, a8, a9) => await core(a1, a2, a3, a4, a5, a6, a7, a8, a9); break;
        case 10: func = async (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10) => await core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10); break;
        case 11: func = async (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11) => await core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11); break;
        case 12: func = async (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12) => await core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12); break;
        case 13: func = async (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13) => await core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13); break;
        case 14: func = async (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14) => await core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14); break;
        case 15: func = async (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15) => await core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15); break;
        case 16: func = async (a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16) => await core(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16); break;
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

function resolveArgs(theArgs) {
    for (let i = 0; i < theArgs.length; i++) {
        if (theArgs[i] === null) continue;
        if (typeof theArgs[i] !== 'object') continue;

        if (theArgs[i].hasOwnProperty("blazorDynamicJavaScriptObjectId")) {
            const objId = theArgs[i]["blazorDynamicJavaScriptObjectId"];
            const names = theArgs[i]["blazorDynamicJavaScriptUnresolvedNames"];

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
    const ret = {
        last,
        target
    };
    return ret;
}
