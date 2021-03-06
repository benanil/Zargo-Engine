
using OpenTK.Mathematics;
using System.Linq;
using System.Reflection;
using ZargoEngine.Helper;
using ZargoEngine.Editor;
using ImGuiNET;
using ZargoEngine.Rendering;

namespace ZargoEngine
{
    public class Component : IDrawable
    {
        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        [NonSerialized]
        public string name;

        public virtual void DrawGUI()
        {
            ImGui.TextColored(Color4.Orange.ToSystem(), name);
            
            SerializeComponent();

            ImGui.Separator();
        }

        protected void SerializeComponent()
        {
            SerializeFields();
            SerializeMethods();
        }

        private void SerializeMethods()
        {
            var Methods = this.GetType().GetMethods();

            for (int i = 0; i < Methods.Length; i++)
            {
                foreach(var item in Methods[i].GetCustomAttributes())
                {
                    if (item is ButtonAttribute button)
                    {
                        // attribute içerisinde özellikle isim belirtilmediyse methodun adını kullan
                        string buttonName = Methods[i].Name == string.Empty ? Methods[i].Name : button.name; 
                        if (ImGui.Button(buttonName , button.size))
                        {
                            Methods[i].Invoke(this,null);
                        }
                    }
                }
            }
        }

        private void SerializeFields()
        {
            FieldInfo[] fields = this.GetType().GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = field.GetValue(this);

                if (value is MeshRenderer) return;

                var attributes = field.GetCustomAttributes().ToArray();

                if (attributes != default && attributes.Length > 0)
                {
                    if (attributes.Any(x => x is NonSerializedAttribute)) continue;

                    DragAttribute dragAttribute = default;
                    SliderAttribute sliderAttribute = default;
                    InputAttribute inputAttribute = default;

                    // drag attribute
                    if (attributes.Any(x =>
                    {
                        if (x.GetType() == typeof(DragAttribute))
                        {
                            dragAttribute = (DragAttribute)x;
                            return true;
                        }
                        return false;
                    }))
                    {
                        switch (value)
                        {
                            case float val:
                                ImGui.DragFloat(field.Name, ref val, dragAttribute.speed, dragAttribute.min, dragAttribute.max, dragAttribute.format);
                                field.SetValue(this, val);
                                continue;
                            case int intVal:
                                ImGui.DragInt(field.Name, ref intVal);
                                field.SetValue(this, intVal);
                                continue;
                            case Vector2 vector2:
                                System.Numerics.Vector2 vec2Val = vector2.ToSystemRef();
                                ImGui.DragFloat2(field.Name, ref vec2Val, dragAttribute.speed, dragAttribute.min, dragAttribute.max, dragAttribute.format);

                                field.SetValue(this, vec2Val.ToOpenTKRef());
                                continue;
                            case Vector3 vector3:
                                System.Numerics.Vector3 vec3Val = vector3.ToSystemRef();
                                ImGui.DragFloat3(field.Name, ref vec3Val, dragAttribute.speed, dragAttribute.min, dragAttribute.max, dragAttribute.format);
                                field.SetValue(this, vec3Val.ToOpenTKRef());
                                continue;
                        }
                    }

                    // slider attribute
                    if (attributes.Any(x =>
                    {
                        if (x.GetType() == typeof(SliderAttribute))
                        {
                            sliderAttribute = (SliderAttribute)x;
                            return true;
                        }
                        return false;
                    }))
                    {
                        switch (value)
                        {
                            case float val:
                                ImGui.SliderFloat(field.Name, ref val, sliderAttribute.min, sliderAttribute.max, sliderAttribute.format, sliderAttribute.imGuiSliderFlags);
                                field.SetValue(this, val);
                                continue;
                            case int intVal:
                                ImGui.SliderInt(field.Name, ref intVal, (int)sliderAttribute.min, (int)sliderAttribute.max, intVal.ToString(), sliderAttribute.imGuiSliderFlags);
                                field.SetValue(this, intVal);
                                continue;
                            case Vector2 vector2:
                                System.Numerics.Vector2 vec2Val = vector2.ToSystemRef();
                                ImGui.SliderFloat2(field.Name, ref vec2Val, sliderAttribute.min, sliderAttribute.max, sliderAttribute.format, sliderAttribute.imGuiSliderFlags);

                                field.SetValue(this, vec2Val.ToOpenTKRef());
                                continue;
                            case Vector3 vector3:
                                System.Numerics.Vector3 vec3Val = vector3.ToSystemRef();
                                ImGui.DragFloat3(field.Name, ref vec3Val);
                                field.SetValue(this, vec3Val.ToOpenTKRef());
                                continue;
                        }
                    }

                    //Input attribute
                    if (attributes.Any(x =>
                    {
                        if (x.GetType() == typeof(InputAttribute))
                        {
                            inputAttribute = (InputAttribute)x;
                            return true;
                        }
                        return false;
                    }))
                    {
                        switch (value)
                        {
                            case float val:
                                ImGui.InputFloat(field.Name, ref val, inputAttribute.step, inputAttribute.step_fast, inputAttribute.format, inputAttribute.flags);
                                field.SetValue(this, val);
                                continue;
                            case int intVal:
                                ImGui.InputInt(field.Name, ref intVal, (int)inputAttribute.step, (int)inputAttribute.step_fast, inputAttribute.flags);
                                field.SetValue(this, intVal);
                                continue;
                            case Vector2 vector2:
                                System.Numerics.Vector2 vec2Val = vector2.ToSystemRef();
                                ImGui.InputFloat2(field.Name, ref vec2Val, inputAttribute.format, inputAttribute.flags);

                                field.SetValue(this, vec2Val.ToOpenTKRef());
                                continue;
                            case Vector3 vector3:
                                System.Numerics.Vector3 vec3Val = vector3.ToSystemRef();
                                ImGui.InputFloat3(field.Name, ref vec3Val, inputAttribute.format, inputAttribute.flags);
                                field.SetValue(this, vec3Val.ToOpenTKRef());
                                continue;
                        }
                    }
                }

                // if has no attribute draw drag 

                switch (value)
                {
                    case float val:
                        ImGui.DragFloat(field.Name, ref val);
                        field.SetValue(this, val);
                        break;
                    case int intVal:
                        ImGui.DragInt(field.Name, ref intVal);
                        field.SetValue(this, intVal);
                        break;
                    case bool boolVal:
                        ImGui.Checkbox(field.Name, ref boolVal);
                        field.SetValue(this, boolVal);
                        break;
                    case string strValue:
                        ImGui.InputText(field.Name, ref strValue, 20);
                        field.SetValue(this, strValue);
                        break;
                    case Vector2 vector2:
                        System.Numerics.Vector2 vec2Val = vector2.ToSystemRef();
                        ImGui.DragFloat2(field.Name, ref vec2Val);

                        field.SetValue(this, vec2Val.ToOpenTKRef());
                        break;
                    case Vector3 vector3:
                        System.Numerics.Vector3 vec3Val = vector3.ToSystemRef();
                        ImGui.DragFloat3(field.Name, ref vec3Val);
                        field.SetValue(this, vec3Val.ToOpenTKRef());
                        break;
                    case Color4 color4:
                        System.Numerics.Vector4 sysColor = color4.ToSystem();
                        ImGui.ColorEdit4(field.Name, ref sysColor);
                        field.SetValue(this, sysColor.ToOpenTkColorRef());
                        break;
                }
            }
        } //SerializeFields end

    }
}
