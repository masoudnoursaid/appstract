namespace ClientSdk.Generator;

public class ProjectModel
{
    public ProjectModel(string name, string nameSpace, string swaggerEndPoint)
    {
        Name = name;
        NameSpace = nameSpace;
        SwaggerEndPoint = swaggerEndPoint;
    }

    public string Name { get; set; }
    public string NameSpace { get; set; }
    public string SwaggerEndPoint { get; set; }
}