# com.agraris.toast
 Easy implementation of Toast on Android for Unity.

## Usage
``` C#
using Agraris.Tools;

void Start()
{
    Toast.ShowToast("Toast Message");
    // or
    Toast.ShowToast("Toast Message", ToastLength.LENGTH_LONG); // LENGTH_SHORT = 2 seconds | LENGTH_LONG = 3.5 seconds
}
```