using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class HttpResponse {
    public string code;
    public string msg;
    public object data;

    public override string ToString() {
        return JsonConvert.SerializeObject(this);
    }
}