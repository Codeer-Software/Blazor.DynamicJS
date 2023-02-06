Blazor.DynamicJS
========

Blazor.DynamicJS is a library for writing JavaScript using C#

## Features ...
### Useing dynamic.
Write code in duck type using dynamic.

### Assign interface.
You can also write code using types by assigning an interface. Just define an interface and Blazor.DynamicJS will use DispatchProxy to provide the implementation.

## Getting Started
... just moment




## samples


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

```cs  
void ElementReferenceAndToJSSample()
{
    dynamic jsElement = _js.ToJS(_elementReferenceSample);
    jsElement.innerText = "test";

    dynamic jsVariable = _js.ToJS(new { a = 0, b = 1 });
}
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
```cs  