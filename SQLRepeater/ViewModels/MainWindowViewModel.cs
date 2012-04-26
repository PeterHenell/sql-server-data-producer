﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Data.Common;
using SQLRepeater.Entities;
using SQLRepeater.DataAccess;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using SQLRepeater.DatabaseEntities.Entities;
using SQLRepeater.Entities.ExecutionOrderEntities;


namespace SQLRepeater.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public DelegateCommand OpenSqlConnectionBuilderCommand { get; private set; }
        public DelegateCommand LoadTablesCommand { get; private set; }
        
        SQLRepeater.Model.ApplicationModel _model;
        public SQLRepeater.Model.ApplicationModel Model
        {
            get
            {
                return _model;
            }
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged("Model");
                }
            }
        }

        ExecutionOrderViewModel _executionOrderVM;
        public ExecutionOrderViewModel ExecutionOrderVM
        {
            get
            {
                return _executionOrderVM;
            }
            set
            {
                if (_executionOrderVM != value)
                {
                    _executionOrderVM = value;
                    OnPropertyChanged("ExecutionOrderVM");
                }
            }
        }


        ObservableCollection<ExecutionDetailsViewModel> _executionDetailsVMs;
        public ObservableCollection<ExecutionDetailsViewModel> ExecutionDetailsVMS
        {
            get
            {
                return _executionDetailsVMs;
            }
            set
            {
                if (_executionDetailsVMs != value)
                {
                    _executionDetailsVMs = value;
                    OnPropertyChanged("ExecutionDetailsVMS");
                }
            }
        }


        ExecutionDetailsViewModel _selectedExecutionDetailsVM;
        public ExecutionDetailsViewModel SelectedExecutionDetailVM
        {
            get
            {
                return _selectedExecutionDetailsVM;
            }
            set
            {
                if (_selectedExecutionDetailsVM != value)
                {
                    _selectedExecutionDetailsVM = value;
                    OnPropertyChanged("SelectedExecutionDetailVM");
                }
            }
        }

        SidePanelViewModel _sidepanelVM;
        public SidePanelViewModel SidePanelVM
        {
            get
            {
                return _sidepanelVM;
            }
            set
            {
                if (_sidepanelVM != value)
                {
                    _sidepanelVM = value;
                    OnPropertyChanged("SidePanelVM");
                }
            }
        }


        private void LoadTables()
        {
            TableEntityDataAccess tda = new TableEntityDataAccess(Model.ConnectionString);
            tda.BeginGetAllTablesAndColumns(res =>
            {
                Model.Tables = res;
                Model.SelectedTable = Model.Tables.FirstOrDefault();
            });
            //ColumnEntityDataAccess cda = new ColumnEntityDataAccess(Model.ConnectionString);
            //foreach (var tabl in Model.Tables)
            //{
            //    cda.BeginGetAllColumnsForTable(tabl, cols =>
            //        {
            //            tabl.Columns = cols;
            //        });
            //}

        }

        public MainWindowViewModel()
        {
            Model = new SQLRepeater.Model.ApplicationModel();
            Model.ExecutionItems = new ObservableCollection<ExecutionItem>();
            Model.SelectedExecutionItem = Model.ExecutionItems.FirstOrDefault();
            
            ExecutionOrderVM = new ExecutionOrderViewModel(Model);
            SidePanelVM = new SidePanelViewModel(Model);

            


            OpenSqlConnectionBuilderCommand = new DelegateCommand(() =>
            {
                ConnectionStringCreatorGUI.SqlConnectionString initialConnStr;

                try
                {
                    initialConnStr = new ConnectionStringCreatorGUI.SqlConnectionString(Model.ConnectionString);
                }
                catch (Exception)
                {
                    initialConnStr = new ConnectionStringCreatorGUI.SqlConnectionString();
                }

                Window win = new ConnectionStringCreatorGUI.ConnectionStringBuilderWindow(initialConnStr, returnConnBuilder =>
                {
                    Model.ConnectionString = returnConnBuilder.ToString();
                });

                win.Show();

            });
            
            LoadTablesCommand = new DelegateCommand(() =>
                {
                    LoadTables();                        
                });

            //OpenValueEditWindowCommand = new DelegateCommand<ColumnEntity>(colEntity =>
            //    {
            //        ColumnEntityValueConfigurationView valueEditView = 
            //            new ColumnEntityValueConfigurationView(
            //                new ValueEntityConfigurationViewModel(colEntity));
            //        valueEditView.Show();
            //    });

        }

    }
}
