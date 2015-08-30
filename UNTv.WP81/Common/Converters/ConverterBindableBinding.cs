using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace UNTv.WP81.Common.Converters
{
    public class ConverterBindableBinding : MarkupExtension
    {
        public Binding Binding { get; set; }

        public IValueConverter Converter { get; set; }

        public Binding ConverterParameterBinding { get; set; }

        public Binding ConverterBinding { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var multiBinding = new MultiBinding();
            multiBinding.Bindings.Add(Binding);
            multiBinding.Bindings.Add(ConverterParameterBinding);

            if (ConverterBinding != null)
                multiBinding.Bindings.Add(ConverterBinding);

            var adapter = new MultiValueConverterAdapter();
            adapter.Converter = Converter;
            multiBinding.Converter = adapter;

            return multiBinding.ProvideValue(serviceProvider);
        }
    }

}
