using Avalonia.Controls;

using Avalonia.RedditMVVM.ViewModels;

using CommunityToolkit.Mvvm.DependencyInjection;

namespace Avalonia.RedditMVVM.Views
{
    public partial class PostWidget : UserControl
    {
        public PostWidget()
        {
            InitializeComponent();
            var vm = Ioc.Default.GetRequiredService<PostWidgetViewModel>()!;
            DataContext = vm;
            AttachedToLogicalTree += (s, e) => vm.IsActive = true;
            DetachedFromLogicalTree += (s, e) => vm.IsActive = false;
        }

        public PostWidgetViewModel? ViewModel => DataContext as PostWidgetViewModel;
    }
}
