using ImGuiNET;
using OpenTK.Mathematics;
using System.Linq;
using System.Reflection;
using ZargoEngine.Helper;
using ZargoEngine.Editor;

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

            SerializeFields();
        }

        private void SerializeFields()
        {
            FieldInfo[] fields = this.GetType().GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = field.GetValue(this);

                var attributes = field.GetCustomAttributes().ToArray();

                if (attributes != default && attributes.Length > 0)
                {
                    if (attributes[0] is NonSerializedAttribute)
                        continue;

                    switch (attributes[0])
                    {
                        case DragAttribute dragAttribute:
                            switch (value)
                            {
                                case float val:
                                    ImGui.DragFloat(field.Name, ref val, dragAttribute.speed, dragAttribute.min, dragAttribute.max, dragAttribute.format);
                                    field.SetValue(this, val);
                                    break;
                                case int intVal:
                                    ImGui.DragInt(field.Name, ref intVal);
                                    field.SetValue(this, intVal);
                                    break;
                                case Vector2 vector2:
                                    System.Numerics.Vector2 vec2Val = vector2.ToSystem();
                                    ImGui.DragFloat2(field.Name, ref vec2Val, dragAttribute.speed, dragAttribute.min, dragAttribute.max, dragAttribute.format);

                                    field.SetValue(this, vec2Val.ToOpenTk());
                                    break;
                                case Vector3 vector3:
                                    System.Numerics.Vector3 vec3Val = vector3.ToSystem();
                                    ImGui.DragFloat3(field.Name, ref vec3Val, dragAttribute.speed, dragAttribute.min, dragAttribute.max, dragAttribute.format);
                                    field.SetValue(this, vec3Val.ToOpenTk());
                                    break;
                                default: goto jump;// string bool vs.
                            }
                            continue;
                        case SliderAttribute sliderAttribute:
                            switch (value)
                            {
                                case float val:
                                    ImGui.SliderFloat(field.Name, ref val, sliderAttribute.min, sliderAttribute.max, sliderAttribute.format, sliderAttribute.imGuiSliderFlags);
                                    field.SetValue(this, val);
                                    break;
                                case int intVal:
                                    ImGui.SliderInt(field.Name, ref intVal, (int)sliderAttribute.min, (int)sliderAttribute.max, intVal.ToString(), sliderAttribute.imGuiSliderFlags );
                                    field.SetValue(this, intVal);
                                    break;
                                case Vector2 vector2:
                                    System.Numerics.Vector2 vec2Val = vector2.ToSystem();
                                    ImGui.SliderFloat2(field.Name, ref vec2Val, sliderAttribute.min, sliderAttribute.max, sliderAttribute.format, sliderAttribute.imGuiSliderFlags);

                                    field.SetValue(this, vec2Val.ToOpenTk());
                                    break;
                                case Vector3 vector3:
                                    System.Numerics.Vector3 vec3Val = vector3.ToSystem();
                                    ImGui.DragFloat3(field.Name, ref vec3Val);
                                    field.SetValue(this, vec3Val.ToOpenTk());
                                    break;
                                default: goto jump;// string bool vs.
                            }
                            continue;
                        case InputAttribute inputAttribute:
                            switch (value)
                            {
                                case float val:
                                    ImGui.InputFloat(field.Name, ref val, inputAttribute.step, inputAttribute.step_fast, inputAttribute.format, inputAttribute.flags);
                                    field.SetValue(this, val);
                                    break;
                                case int intVal:
                                    ImGui.InputInt(field.Name, ref intVal, (int)inputAttribute.step, (int)inputAttribute.step_fast, inputAttribute.flags);
                                    field.SetValue(this, intVal);
                                    break;
                                case Vector2 vector2:
                                    System.Numerics.Vector2 vec2Val = vector2.ToSystem();
                                    ImGui.InputFloat2(field.Name, ref vec2Val, inputAttribute.format, inputAttribute.flags);

                                    field.SetValue(this, vec2Val.ToOpenTk());
                                    break;
                                case Vector3 vector3:
                                    System.Numerics.Vector3 vec3Val = vector3.ToSystem();
                                    ImGui.InputFloat3(field.Name, ref vec3Val, inputAttribute.format, inputAttribute.flags);
                                    field.SetValue(this, vec3Val.ToOpenTk());
                                    break;
                                default: goto jump; // string bool vs.
                            }
                            continue;
                    }
                }

            jump:
                // drag input slider

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
                        System.Numerics.Vector2 vec2Val = vector2.ToSystem();
                        ImGui.DragFloat2(field.Name, ref vec2Val);

                        field.SetValue(this, vec2Val.ToOpenTk());
                        break;
                    case Vector3 vector3:
                        System.Numerics.Vector3 vec3Val = vector3.ToSystem();
                        ImGui.DragFloat3(field.Name, ref vec3Val);
                        field.SetValue(this, vec3Val.ToOpenTk());
                        break;
                    case Color4 color4:
                        System.Numerics.Vector4 sysColor = color4.ToSystem();
                        ImGui.ColorEdit4(field.Name, ref sysColor);
                        field.SetValue(this, sysColor.ToOpenTkColor());
                        break;
                }
            }
        } //SerializeFields end

    }
}
