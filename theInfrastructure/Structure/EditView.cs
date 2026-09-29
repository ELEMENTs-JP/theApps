using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace theInfrastructure
{

    public interface IEditView
    {
        string Name { get; set; }
        string Title { get; set; }
        string Typ { get; set; }
        string Description { get; set; }
    }

    public class EditView : IEditView
    {
        public EditView()
        { }

        public string Name { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Typ { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    
  
    }



}
