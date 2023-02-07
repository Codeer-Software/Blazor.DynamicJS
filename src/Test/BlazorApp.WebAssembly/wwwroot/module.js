export class Rectangle {
    constructor(height, width) {
        this.height = height;
        this.width = width;
    }
}

export function sum(...theArgs) {
    let total = 0;
    for (const arg of theArgs) {
        total += arg;
    }
    return total;
}

export let data = 100;
