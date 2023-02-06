Blazor.DynamicJS
========

Blazor.DynamicJS is a library for writing JavaScript using C#

## Features ...
### Useing dynamic.
Write code in duck typing using dynamic.

### Assign interface.
You can also write code using types by assigning an interface. Just define an interface and Blazor.DynamicJS will use DispatchProxy to provide the implementation.

## Getting Started
... just moment




## Samples

### Initialize
```cs  
DynamicJSRuntime? _js;

protected override async Task OnInitializedAsync()
{
    await base.OnInitializedAsync();
    _js = await JS.CreateDymaicRuntimeAsync();
}

public ValueTask DisposeAsync()
    => _js?.DisposeAsync() ?? ValueTask.CompletedTask;
```

### Manipulate dom
```cs  
void DomSample()
{
    dynamic window = _js.GetWindow();

    dynamic button = window.document.createElement("button");
    button.innerText = "new button";

    dynamic div = window.document.getElementById("parent-div");
    div.append(button);

    //call back
    button.addEventListener("click", (Action<dynamic>)(e => {
        string detail = e.detail.toString();
        window.console.log($"clicked {detail}");
    }));
}
```

### Global
```html  
<script>
    TestTargets =
    {
        Rectangle: class {
            constructor(height, width) {
                this.height = height;
                this.width = width;
            }
        },

        sum: function (...theArgs) {
            let total = 0;
            for (const arg of theArgs) {
                total += arg;
            }
            return total;
        },

        data: 100,

        list:[1, 2, 3, 4],
    }
 </script>
```

```cs  
void GlobalSample()
{
    dynamic window = _js.GetWindow();
    dynamic targets = window.TestTargets;

    //call function
    int sum = targets.sum(1, 2, 3, 4);
    window.console.log(sum);

    //new class
    dynamic rect = new JSSyntax(targets.Rectangle).New(10, 20);
    window.console.log(rect.height);
    window.console.log(rect.width);

    //set data
    targets.data = 100;

    //get data
    int data = targets.data;
    window.console.log(data);

    //array
    for (int i = 0; i < (int)targets.list.length; i++)
    {
        window.console.log(targets.list[i]);
    }

    //add data
    window.newData = new { num = 100, text = "text" };
    window.console.log(window.newData.num);
    window.console.log(window.newData.text);
}
```

### Module
```js  
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

export const list = [1, 2, 3, 4];
```

```cs  
async Task ModuleSample()
{
    //Please specify with absolute path
    dynamic mod = await _js.ImportAsync("/module.js");

    //call function
    int sum = mod.sum(1, 2, 3, 4);

    //new class
    dynamic rect = new JSSyntax(mod.Rectangle).New(10, 20);

    //get data
    int data = mod.data;

    //array
    for (int i = 0; i < (int)mod.list.length; i++)
    {
        int val = mod.list[i];
    }
}
```

### Send Variable from C# to JavaScript. It can also send ElementReference.
```html  
<div @ref="_elementReferenceSample">sample element reference</div>
```
```cs 
void ElementReferenceAndToJSSample()
{
    dynamic jsElement = _js.ToJS(_elementReferenceSample);
    jsElement.innerText = "test";

    dynamic jsVariable = _js.ToJS(new { a = 0, b = 1 });
}
```

### Async
```cs  
async Task AsyncSample()
{
    dynamic targets = _js.GetWindow().TestTargets;

    //call function
    int sum = await new JSSyntax(targets.sum).InvokeAsync<int>(1, 2, 3, 4); 
    
    //void or not use result
    await new JSSyntax(targets.sum).InvokeAsync(1, 2, 3, 4);

    //syntax sugar
    int sum2 = await targets.sum(1, 2, 3, 4, new JSAsync<int>());
    await targets.sum(1, 2, 3, 4, new JSAsync());

    //new class
    dynamic rect = await new JSSyntax(targets.Rectangle).NewAsync(10, 20);

    //set data
    await new JSSyntax(targets.data).SetValueAsync(100);

    //get data
    int data = await new JSSyntax(targets.data).GetValueAsync<int>();

    //array
    int length = await new JSSyntax(targets.list.length).GetValueAsync<int>();
    for (int i = 0; i < length; i++)
    {
        int val = await new JSSyntax(targets.list).GetIndexValueAsync<int>(i);
    }
}
```

### Assign Interface

```cs  
[JSCamelCase]
public interface IArray
{
    int this[int index] { get; set; }
    int Length { get; set; }
    Task set_ItemAsync(int index, int value);
    Task<int> get_ItemAsync(int index);
}

public interface IRectangle
{ 
    int Height { get; set; }
    int Width { get; set; }
}

[JSCamelCase]
public interface ITestTargets
{
    [JSConstructor, JSIgnoreCase]
    IRectangle Rectangle(int h, int w);

    int Sum(params int[] values);
    Task<int> SumAsync(params int[] values);

    int Data { get; set; }

    IArray List { get; set; }
}
```

```cs  
async Task AssignInterfaceSample()
{
    ITestTargets targets = new JSSyntax(_js.GetWindow().TestTargets).AssignInterface<ITestTargets>();

    //call function
    int sum = targets.Sum(1, 2, 3, 4);
    int sum2 = await targets.SumAsync(1, 2, 3, 4);

    //new class
    IRectangle rect = targets.Rectangle(10, 20);

    //set data
    targets.Data = 100;

    //get data
    int data = targets.Data;

    //array
    for (int i = 0; i < (int)targets.List.Length; i++)
    {
        int val = targets.List[i];
    }
    //New, ToJS, ImportModule, GetWindoew
}
```
