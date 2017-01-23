namespace xofz.Journal.Root
{
    using System;
    using System.Reflection;
    using System.Windows.Forms;
    using xofz.Journal.UI.Forms;

    internal static class EntryPoint
    {
        [STAThread]
        private static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) => loadEmbeddedAssembly(e.Name);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var bootstrapper = new FormsBootstrapper();
            bootstrapper.Bootstrap();

            Application.Run(bootstrapper.Shell);
        }

        private static Assembly loadEmbeddedAssembly(string name)
        {
            var assemblyName = new AssemblyName(name);
            if (name.EndsWith("Retargetable=Yes"))
            {
                return Assembly.Load(assemblyName);
            }

            var container = Assembly.GetExecutingAssembly();
            var path = assemblyName.Name + ".dll";

            using (var stream = container.GetManifestResourceStream(path))
            {
                if (stream == null)
                {
                    return null;
                }

                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return Assembly.Load(bytes);
            }
        }
    }
}
