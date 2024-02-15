using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;

namespace ChickenDinnerV2.Core.Tools
{
    class ILBuilder
    {
        protected Dictionary<string, Label> Labels { get; set; } = new Dictionary<string, Label>();
        protected List<CodeInstruction> Result { get; set; }
        protected List<CodeInstruction> Source { get; set; }
        protected ILGenerator Generator { get; set; }
        protected MethodBase Original { get; set; }

        public ILBuilder(List<CodeInstruction> instructions, ILGenerator generator = null, MethodBase original = null)
        {
            Result = new List<CodeInstruction>();
            Source = instructions;
            Generator = generator;
            Original = original;
        }

        public ILBuilder Copy(int position = -1, int from = 0, int count = -1)
        {
            if (count < 0)
            {
                count = Source.Count - from;
            }
            if (position < 0)
            {
                position = Result.Count;
            }

            int sourcePos;

            for (sourcePos = from; sourcePos < from + count && position < Result.Count; sourcePos++, position++)
            {
                Result.Insert(position, Source[sourcePos]);
            }
            for (; sourcePos < from + count; sourcePos++)
            {
                Result.Add(Source[sourcePos]);
            }

            return this;
        }

        public CodeInstruction[] GetResult()
        {
            return Result.ToArray();
        }

        private void InsertOrAdd(CodeInstruction instruction, int idx)
        {
            if (idx > -1)
            {
                Result.Insert(idx, instruction);
            }
            else
            {
                Result.Add(instruction);
            }
        }

        public ILBuilder Custom(CodeInstruction instruction, int idx = -1)
        {
            InsertOrAdd(instruction, idx);
            return this;
        }



        public ILBuilder Newobj(Type type, int constructorId, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Newobj, AccessTools.GetDeclaredConstructors(type)[constructorId]), idx);
            return this;
        }



        public ILBuilder Ldarg(int argNb, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldarg, argNb), idx);
            return this;
        }

        public ILBuilder Ldarg_S(int argNb, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldarg_S, argNb), idx);
            return this;
        }

        public ILBuilder Ldloc(int locNb, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldloc, locNb), idx);
            return this;
        }

        public ILBuilder Ldloca(int locNb, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldloca, locNb), idx);
            return this;
        }

        public ILBuilder Ldloc_S(int locNb, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldloc_S, locNb), idx);
            return this;
        }

        public ILBuilder Ldloca_S(int locNb, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldloca_S, locNb), idx);
            return this;
        }

        public ILBuilder Stloc(int locNb, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Stloc, locNb), idx);
            return this;
        }
        public ILBuilder Stind_Ref(int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Stind_Ref), idx);
            return this;
        }



        public ILBuilder Ldfld(Type typeClass, string field, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldfld, AccessTools.Field(typeClass, field)), idx);
            return this;
        }

        public ILBuilder Ldsfld(Type typeClass, string field, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldsfld, AccessTools.Field(typeClass, field)), idx);
            return this;
        }

        public ILBuilder Ldflda(Type typeClass, string field, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldflda, AccessTools.Field(typeClass, field)), idx);
            return this;
        }

        public ILBuilder Ldsflda(Type typeClass, string field, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldsflda, AccessTools.Field(typeClass, field)), idx);
            return this;
        }

        public ILBuilder Ldstr(string str, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldstr, str), idx);
            return this;
        }

        public ILBuilder Ldc_I4(int nb, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ldc_I4, nb), idx);
            return this;
        }

        public ILBuilder Pop(int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Pop), idx);
            return this;
        }



        public ILBuilder Call(Type typeClass, string method, Type[] parameters = null, int idx = -1)
        {
            List<Type> resultParameters = new List<Type>();

            if (parameters == null)
            {
                ParameterInfo[] paramsInfo = typeClass.GetMethod(method).GetParameters();
                foreach (ParameterInfo param in paramsInfo)
                {
                    resultParameters.Add(param.ParameterType);
                }
            }
            else
            {
                resultParameters = parameters.ToList();
            }

            Type[] generics = typeClass.GetMethod(method, resultParameters.ToArray()).GetGenericArguments();
            if (generics != null && generics.Length > 0)
            {
                InsertOrAdd(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeClass, method, resultParameters.ToArray(), typeClass.GetMethod(method).GetGenericArguments())), idx);
            }
            else
            {
                InsertOrAdd(new CodeInstruction(OpCodes.Call, AccessTools.Method(typeClass, method, resultParameters.ToArray())), idx);
            }

            return this;
        }
        public ILBuilder Call_Prop(Type typeClass, string property, int idx = -1)
        {

            InsertOrAdd(new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeClass, property)), idx);
            return this;
        }

        public ILBuilder Callvirt(Type typeClass, string method, Type[] parameters = null, int idx = -1)
        {
            List<Type> resultParameters = new List<Type>();

            if (parameters == null)
            {
                ParameterInfo[] paramsInfo = typeClass.GetMethod(method).GetParameters();
                foreach (ParameterInfo param in paramsInfo)
                {
                    resultParameters.Add(param.ParameterType);
                }
            }
            else
            {
                resultParameters = parameters.ToList();
            }

            Type[] generics = typeClass.GetMethod(method, resultParameters.ToArray()).GetGenericArguments();
            if (generics != null && generics.Length > 0)
            {
                InsertOrAdd(new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeClass, method, resultParameters.ToArray(), typeClass.GetMethod(method).GetGenericArguments())), idx);
            }
            else
            {
                InsertOrAdd(new CodeInstruction(OpCodes.Callvirt, AccessTools.Method(typeClass, method, resultParameters.ToArray())), idx);
            }

            return this;
        }
        public ILBuilder Callvirt_Prop(Type typeClass, string property, int idx = -1)
        {

            InsertOrAdd(new CodeInstruction(OpCodes.Callvirt, AccessTools.PropertyGetter(typeClass, property)), idx);
            return this;
        }



        public ILBuilder DefineLabel(string name, int fieldNb = -1)
        {
            Labels.Add(name, Generator.DefineLabel());
            if (fieldNb > -1)
            {
                Result[fieldNb].labels.Add(Labels[name]);
            }
            else
            {
                Result.Last().labels.Add(Labels[name]);
            }
            return this;
        }



        public ILBuilder Brtrue(string label, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Brtrue, Labels[label]), idx);
            return this;
        }

        public ILBuilder Brtrue_S(string label, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Brtrue_S, Labels[label]), idx);
            return this;
        }

        public ILBuilder Brfalse(string label, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Brfalse, Labels[label]), idx);
            return this;
        }

        public ILBuilder Brfalse_S(string label, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Brfalse_S, Labels[label]), idx);
            return this;
        }

        public ILBuilder Br(string label, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Br, Labels[label]), idx);
            return this;
        }

        public ILBuilder Br_S(string label, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Br_S, Labels[label]), idx);
            return this;
        }

        public ILBuilder Beq_S(string label, int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Beq_S, Labels[label]), idx);
            return this;
        }



        public ILBuilder Dup(int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Dup), idx);
            return this;
        }


        public ILBuilder Ret(int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Ret), idx);
            return this;
        }

        public ILBuilder Mul(int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Mul), idx);
            return this;
        }

        public ILBuilder Div(int idx = -1)
        {
            InsertOrAdd(new CodeInstruction(OpCodes.Div), idx);
            return this;
        }
    }
}
