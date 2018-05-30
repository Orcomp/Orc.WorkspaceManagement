﻿[assembly: System.Resources.NeutralResourcesLanguageAttribute("en-US")]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETFramework,Version=v4.6", FrameworkDisplayName=".NET Framework 4.6")]
[assembly: System.Windows.Markup.XmlnsDefinitionAttribute("http://schemas.wildgums.com/orc/workspacemanagement", "Orc.WorkspaceManagement")]
[assembly: System.Windows.Markup.XmlnsDefinitionAttribute("http://schemas.wildgums.com/orc/workspacemanagement", "Orc.WorkspaceManagement.Behaviors")]
[assembly: System.Windows.Markup.XmlnsDefinitionAttribute("http://schemas.wildgums.com/orc/workspacemanagement", "Orc.WorkspaceManagement.Converters")]
[assembly: System.Windows.Markup.XmlnsDefinitionAttribute("http://schemas.wildgums.com/orc/workspacemanagement", "Orc.WorkspaceManagement.Views")]
[assembly: System.Windows.Markup.XmlnsPrefixAttribute("http://schemas.wildgums.com/orc/workspacemanagement", "orcworkspacemanagement")]
[assembly: System.Windows.ThemeInfoAttribute(System.Windows.ResourceDictionaryLocation.None, System.Windows.ResourceDictionaryLocation.SourceAssembly)]
public class static ModuleInitializer
{
    public static void Initialize() { }
}
namespace Orc.WorkspaceManagement.Behaviors
{
    public class AutoWorkspace : Orc.WorkspaceManagement.Behaviors.WorkspaceBehaviorBase<System.Windows.FrameworkElement>
    {
        public static readonly System.Windows.DependencyProperty PersistGridSettingsProperty;
        public static readonly System.Windows.DependencyProperty PersistSizeProperty;
        public AutoWorkspace() { }
        public bool PersistGridSettings { get; set; }
        public bool PersistSize { get; set; }
        protected override void LoadSettings(Orc.WorkspaceManagement.IWorkspace workspace, string prefix) { }
        protected override void SaveSettings(Orc.WorkspaceManagement.IWorkspace workspace, string prefix) { }
    }
    public class AutoWorkspaceGrid : Orc.WorkspaceManagement.Behaviors.WorkspaceBehaviorBase<System.Windows.Controls.Grid>
    {
        public static readonly System.Windows.DependencyProperty ColumnsToPersistProperty;
        public static readonly System.Windows.DependencyProperty RowsToPersistProperty;
        public AutoWorkspaceGrid() { }
        public string ColumnsToPersist { get; set; }
        public string RowsToPersist { get; set; }
        protected override void LoadSettings(Orc.WorkspaceManagement.IWorkspace workspace, string prefix) { }
        protected override void OnAssociatedObjectLoaded() { }
        protected override void SaveSettings(Orc.WorkspaceManagement.IWorkspace workspace, string prefix) { }
    }
    public interface IWorkspaceBehavior
    {
        void Load(Orc.WorkspaceManagement.IWorkspace workspace);
        void Save(Orc.WorkspaceManagement.IWorkspace workspace);
    }
    public abstract class WorkspaceBehaviorBase<T> : Catel.Windows.Interactivity.BehaviorBase<T>, Orc.WorkspaceManagement.Behaviors.IWorkspaceBehavior
        where T : System.Windows.FrameworkElement
    {
        public static readonly System.Windows.DependencyProperty KeyPrefixProperty;
        protected WorkspaceBehaviorBase() { }
        public string KeyPrefix { get; set; }
        protected Orc.WorkspaceManagement.IWorkspaceManager WorkspaceManager { get; }
        public void Load(Orc.WorkspaceManagement.IWorkspace workspace) { }
        protected abstract void LoadSettings(Orc.WorkspaceManagement.IWorkspace workspace, string prefix);
        protected override void OnAssociatedObjectLoaded() { }
        protected override void OnAssociatedObjectUnloaded() { }
        public void Save(Orc.WorkspaceManagement.IWorkspace workspace) { }
        protected abstract void SaveSettings(Orc.WorkspaceManagement.IWorkspace workspace, string prefix);
    }
}
namespace Orc.WorkspaceManagement
{
    public class BehaviorWorkspaceProvider : Orc.WorkspaceManagement.WorkspaceProviderBase
    {
        public BehaviorWorkspaceProvider(Orc.WorkspaceManagement.IWorkspaceManager workspaceManager, Orc.WorkspaceManagement.Behaviors.IWorkspaceBehavior workspaceBehavior, Catel.Services.IDispatcherService dispatcherService, Catel.IoC.IServiceLocator serviceLocator) { }
        public override System.Threading.Tasks.Task ApplyWorkspaceAsync(Orc.WorkspaceManagement.IWorkspace workspace) { }
        public override System.Threading.Tasks.Task ProvideInformationAsync(Orc.WorkspaceManagement.IWorkspace workspace) { }
    }
    public class static FrameworkElementExtensions
    {
        public static void LoadGridValuesFromWorkspace(this System.Windows.FrameworkElement frameworkElement, Orc.WorkspaceManagement.IWorkspace workspace = null, string prefix = null) { }
        public static void LoadSizeFromWorkspace(this System.Windows.FrameworkElement frameworkElement, Orc.WorkspaceManagement.IWorkspace workspace = null, string prefix = null) { }
        public static T LoadValueFromWorkspace<T>(this System.Windows.FrameworkElement frameworkElement, string name, T defaultValue, Orc.WorkspaceManagement.IWorkspace workspace = null, string prefix = null) { }
        public static void SaveGridValuesToWorkspace(this System.Windows.FrameworkElement frameworkElement, Orc.WorkspaceManagement.IWorkspace workspace = null, string prefix = null) { }
        public static void SaveSizeToWorkspace(this System.Windows.FrameworkElement frameworkElement, Orc.WorkspaceManagement.IWorkspace workspace = null, string prefix = null) { }
        public static void SaveValueToWorkspace(this System.Windows.FrameworkElement frameworkElement, string name, object value, Orc.WorkspaceManagement.IWorkspace workspace = null, string prefix = null) { }
    }
}
namespace Orc.WorkspaceManagement.Converters
{
    public class IsCurrentWorkspaceToBooleanConverter : Catel.MVVM.Converters.ValueConverterBase
    {
        public IsCurrentWorkspaceToBooleanConverter() { }
        protected override object Convert(object value, System.Type targetType, object parameter) { }
    }
    public class IsCurrentWorkspaceToCollapsingVisibilityConverter : Catel.MVVM.Converters.VisibilityConverterBase
    {
        public IsCurrentWorkspaceToCollapsingVisibilityConverter() { }
        protected override bool IsVisible(object value, System.Type targetType, object parameter) { }
    }
    public class IsCurrentWorkspaceToHidingVisibilityConverter : Catel.MVVM.Converters.VisibilityConverterBase
    {
        public IsCurrentWorkspaceToHidingVisibilityConverter() { }
        protected override bool IsVisible(object value, System.Type targetType, object parameter) { }
    }
    public class TriggerConverter : System.Windows.Data.IMultiValueConverter
    {
        public TriggerConverter() { }
        public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture) { }
        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) { }
    }
    public class UnderscoreToDoubleUnderscoresStringConverter : Catel.MVVM.Converters.ValueConverterBase<string>
    {
        public UnderscoreToDoubleUnderscoresStringConverter() { }
        protected override object Convert(string value, System.Type targetType, object parameter) { }
    }
}
namespace Orc.WorkspaceManagement.ViewModels
{
    public class WorkspacesViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData AvailableWorkspacesProperty;
        public static readonly Catel.Data.PropertyData ScopeProperty;
        public static readonly Catel.Data.PropertyData SelectedWorkspaceProperty;
        public WorkspacesViewModel(Orc.WorkspaceManagement.IWorkspaceManager workspaceManager, Catel.Services.IUIVisualizerService uiVisualizerService, Catel.IoC.IServiceLocator serviceLocator, Catel.Services.IDispatcherService dispatcherService, Catel.Services.IMessageService messageService, Catel.Services.ILanguageService languageService) { }
        public Catel.Collections.FastObservableCollection<Orc.WorkspaceManagement.IWorkspace> AvailableWorkspaces { get; }
        public Catel.MVVM.TaskCommand<Orc.WorkspaceManagement.IWorkspace> EditWorkspace { get; }
        public Catel.MVVM.TaskCommand<Orc.WorkspaceManagement.IWorkspace> Refresh { get; }
        public Catel.MVVM.TaskCommand<Orc.WorkspaceManagement.IWorkspace> RemoveWorkspace { get; }
        public object Scope { get; set; }
        public Orc.WorkspaceManagement.IWorkspace SelectedWorkspace { get; set; }
        protected override System.Threading.Tasks.Task CloseAsync() { }
        protected override System.Threading.Tasks.Task InitializeAsync() { }
    }
    public class WorkspaceViewModel : Catel.MVVM.ViewModelBase
    {
        public static readonly Catel.Data.PropertyData WorkspaceProperty;
        public static readonly Catel.Data.PropertyData WorkspaceTitleProperty;
        public WorkspaceViewModel(Orc.WorkspaceManagement.IWorkspace workspace, Catel.Services.ILanguageService languageService) { }
        [Catel.MVVM.ModelAttribute()]
        public Orc.WorkspaceManagement.IWorkspace Workspace { get; }
        [Catel.MVVM.ViewModelToModelAttribute("Workspace", "Title")]
        public string WorkspaceTitle { get; set; }
        protected override void ValidateFields(System.Collections.Generic.List<Catel.Data.IFieldValidationResult> validationResults) { }
    }
}
namespace Orc.WorkspaceManagement.Views
{
    public class WorkspacesView : Catel.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {
        public static readonly System.Windows.DependencyProperty HasRefreshButtonProperty;
        public static readonly System.Windows.DependencyProperty ScopeProperty;
        public WorkspacesView() { }
        public bool HasRefreshButton { get; set; }
        [Catel.MVVM.Views.ViewToViewModelAttribute("", MappingType=Catel.MVVM.Views.ViewToViewModelMappingType.ViewToViewModel)]
        public object Scope { get; set; }
        public void InitializeComponent() { }
    }
    public class WorkspaceWindow : Catel.Windows.DataWindow, System.Windows.Markup.IComponentConnector
    {
        public WorkspaceWindow() { }
        public WorkspaceWindow(Orc.WorkspaceManagement.ViewModels.WorkspaceViewModel viewModel) { }
        public void InitializeComponent() { }
    }
}