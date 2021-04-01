using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//using KinoDataModel;
using KinoDataLibrary;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;
using System.IO;
using System.Diagnostics;
using System.Reflection;


namespace KinoUI
{
	public enum BreedChildEnum
	{
		Hair,
		Color,
		Size
	}

	public static class MyCommonDB
	{
		//private static KinologyModel mod = null;

		#region private definitions
		private static KinoDataSet _kinodataset = new KinoDataSet();

		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_trialTableAdapter _tbl_pedigree_trialTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_trial_typeTableAdapter _tbl_trial_typeTableAdapter = null;

		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_champion_statusTableAdapter _tbl_pedigree_champion_statusTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_championStatusesTableAdapter _tbl_championStatusesTableAdapter = null;

		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_vaccineTableAdapter _tbl_pedigree_vaccineTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_vaccine_typeTableAdapter _tbl_vaccine_typeTableAdapter = null;

		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_healthtestTableAdapter _tbl_pedigree_healthtestTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_healthtest_typeTableAdapter _tbl_healthtest_typeTableAdapter = null;

		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_ectoparasitTableAdapter _tbl_pedigree_ectoparasitTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_ectoparasit_typeTableAdapter _tbl_ectoparasit_typeTableAdapter = null;

		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_dewormingTableAdapter _tbl_pedigree_dewormingTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_deworming_typeTableAdapter _tbl_deworming_typeTableAdapter = null;

		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_color_type_colorTableAdapter _tbl_color_type_colorTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.View_ExhStatusesForReviewListTableAdapter _View_ExhStatusesForReviewListTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_organizationTableAdapter _tbl_organizationTableAdapter = null;

		private static KinoDataLibrary.KinoDataSetTableAdapters.View_DogsListForExhibitionsTableAdapter _View_DogsListForExhibitionsTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.View_BreedsWithDescriptionsTableAdapter _View_BreedsWithDescriptionsTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_country_codeTableAdapter _tbl_country_codeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_colorTableAdapter _tbl_colorTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_cityTableAdapter _tbl_cityTableAdapter=null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_countryTableAdapter _tbl_countryTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_agegroupTableAdapter _tbl_agegroupTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_hair_typeTableAdapter _tbl_hair_typeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_size_typeTableAdapter _tbl_size_typeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_color_typeTableAdapter _tbl_color_typeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_groupTableAdapter _tbl_breed_groupTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_nameTableAdapter _tbl_breed_nameTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_exh_statusTableAdapter _tbl_exh_statusTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_exhib_categoryTableAdapter _tbl_exhib_categoryTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_nationalityTableAdapter _tbl_nationalityTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_registration_counterTableAdapter _tbl_registration_counterTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_breedTableAdapter _tbl_breedTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_hairtypeTableAdapter _tbl_breed_hairtypeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_colortypeTableAdapter _tbl_breed_colortypeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_sizetypeTableAdapter _tbl_breed_sizetypeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_sex_typeTableAdapter _tbl_sex_typeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_personTableAdapter _tbl_personTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_kennel_clubTableAdapter _tbl_kennel_clubTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_kennel_breednameTableAdapter _tbl_kennel_breednameTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_kennel_club_PersonTableAdapter _tbl_kennel_club_PersonTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.Func_PersonStatusEnumsTableAdapter _func_PersonStatusEnumsTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigreeTableAdapter _tbl_pedigreeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.Func_DogStatusEnumsTableAdapter _dogStatusEnumsTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_document_typeTableAdapter _tbl_document_typeTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_documentTableAdapter _tbl_pedigree_documentTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_firstlevel_achive_templatesTableAdapter _tbl_firstlevel_achive_templatesTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_next_level_achive_status_templatesTableAdapter _tbl_next_level_achive_status_templatesTableAdapter = null;

		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_exhibitionTableAdapter _tbl_exhibitionTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.View_DogsWithExtendedDescriptionTableAdapter _View_DogsWithExtendedDescriptionTableAdapter = null;
		//
		private static KinoDataLibrary.KinoDataSetTableAdapters.Func_ExhibitionStatusEnumsTableAdapter _Func_ExhibitionStatusEnumsTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.Func_DogExhibitionStatusEnumsTableAdapter _Func_DogExhibitionStatusEnumsTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_exhibitionTableAdapter _tbl_pedigree_exhibitionTableAdapter = null;
		private static KinoDataLibrary.KinoDataSetTableAdapters.Func_AgeGroupsbyDatesTableAdapter _Func_AgeGroupsbyDatesTableAdapter = null;

		#endregion

		#region Deworming Statuses
		public static KinoDataSet.tbl_deworming_typeDataTable DewormingTypeDT()
		{
			if (_tbl_deworming_typeTableAdapter == null)
			{
				_tbl_deworming_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_deworming_typeTableAdapter();
				_tbl_deworming_typeTableAdapter.Fill(_kinodataset.tbl_deworming_type);
			}
			return _kinodataset.tbl_deworming_type;
		}

		public static bool SaveDewormingTypes()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_deworming_type"] != null && _kinodataset.GetChanges().Tables["tbl_deworming_type"].Rows.Count > 0)
				{
					if (_tbl_deworming_typeTableAdapter.Update(_kinodataset.tbl_deworming_type) > 0)
					{
						_kinodataset.tbl_deworming_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_deworming_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveDewormingTypes");
				_kinodataset.tbl_deworming_type.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_pedigree_dewormingDataTable PedigreeDewormingsDT()
		{
			DewormingTypeDT();
			if (_tbl_pedigree_dewormingTableAdapter == null)
			{
				_tbl_pedigree_dewormingTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_dewormingTableAdapter();
				_tbl_pedigree_dewormingTableAdapter.Fill(_kinodataset.tbl_pedigree_deworming);
			}
			return _kinodataset.tbl_pedigree_deworming;
		}

		public static bool SavePedigreeDewormings()
		{
			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree_deworming"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree_deworming"].Rows.Count > 0)
				{
					if (_tbl_pedigree_dewormingTableAdapter.Update(_kinodataset.tbl_pedigree_deworming) > 0)
					{
						_kinodataset.tbl_pedigree_deworming.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree_deworming.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SavePedigreeDewormings");
				_kinodataset.tbl_pedigree_deworming.RejectChanges();
				return false;
			}
		}
		#endregion

		#region Ectoparasit Statuses
		public static KinoDataSet.tbl_ectoparasit_typeDataTable EctoparasitTypeDT()
		{
			if (_tbl_ectoparasit_typeTableAdapter == null)
			{
				_tbl_ectoparasit_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_ectoparasit_typeTableAdapter();
				_tbl_ectoparasit_typeTableAdapter.Fill(_kinodataset.tbl_ectoparasit_type);
			}
			return _kinodataset.tbl_ectoparasit_type;
		}

		public static bool SaveEctoparasitTypes()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_ectoparasit_type"] != null && _kinodataset.GetChanges().Tables["tbl_ectoparasit_type"].Rows.Count > 0)
				{
					if (_tbl_ectoparasit_typeTableAdapter.Update(_kinodataset.tbl_ectoparasit_type) > 0)
					{
						_kinodataset.tbl_ectoparasit_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_ectoparasit_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveEctoparasitTypes");
				_kinodataset.tbl_ectoparasit_type.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_pedigree_ectoparasitDataTable PedigreeEctoparasitsDT()
		{
			EctoparasitTypeDT();
			if (_tbl_pedigree_ectoparasitTableAdapter == null)
			{
				_tbl_pedigree_ectoparasitTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_ectoparasitTableAdapter();
				_tbl_pedigree_ectoparasitTableAdapter.Fill(_kinodataset.tbl_pedigree_ectoparasit);
			}
			return _kinodataset.tbl_pedigree_ectoparasit;
		}

		public static bool SavePedigreeEctoparasits()
		{
			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree_ectoparasit"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree_ectoparasit"].Rows.Count > 0)
				{
					if (_tbl_pedigree_ectoparasitTableAdapter.Update(_kinodataset.tbl_pedigree_ectoparasit) > 0)
					{
						_kinodataset.tbl_pedigree_ectoparasit.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree_ectoparasit.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SavePedigreeEctoparasits");
				_kinodataset.tbl_pedigree_ectoparasit.RejectChanges();
				return false;
			}
		}
		#endregion

		#region Vaccination Statuses
		public static KinoDataSet.tbl_vaccine_typeDataTable VaccineTypeDT()
		{
			if (_tbl_vaccine_typeTableAdapter == null)
			{
				_tbl_vaccine_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_vaccine_typeTableAdapter();
				_tbl_vaccine_typeTableAdapter.Fill(_kinodataset.tbl_vaccine_type);
			}
			return _kinodataset.tbl_vaccine_type;
		}

		public static bool SaveVaccineTypes()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_vaccine_type"] != null && _kinodataset.GetChanges().Tables["tbl_vaccine_type"].Rows.Count > 0)
				{
					if (_tbl_vaccine_typeTableAdapter.Update(_kinodataset.tbl_vaccine_type) > 0)
					{
						_kinodataset.tbl_vaccine_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_vaccine_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveVaccineTypes");
				_kinodataset.tbl_vaccine_type.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_pedigree_vaccineDataTable PedigreeVaccinesDT()
		{
			VaccineTypeDT();
			if (_tbl_pedigree_vaccineTableAdapter == null)
			{
				_tbl_pedigree_vaccineTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_vaccineTableAdapter();
				_tbl_pedigree_vaccineTableAdapter.Fill(_kinodataset.tbl_pedigree_vaccine);
			}
			return _kinodataset.tbl_pedigree_vaccine;
		}

		public static bool SavePedigreeVaccines()
		{
			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree_vaccine"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree_vaccine"].Rows.Count > 0)
				{
					if (_tbl_pedigree_vaccineTableAdapter.Update(_kinodataset.tbl_pedigree_vaccine) > 0)
					{
						_kinodataset.tbl_pedigree_vaccine.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree_vaccine.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SavePedigreeVaccines");
				_kinodataset.tbl_pedigree_vaccine.RejectChanges();
				return false;
			}
		}
		#endregion

		#region Healthtest Statuses
		public static KinoDataSet.tbl_healthtest_typeDataTable Healthtest_typeDT()
		{
			if (_tbl_healthtest_typeTableAdapter == null)
			{
				_tbl_healthtest_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_healthtest_typeTableAdapter();
				_tbl_healthtest_typeTableAdapter.Fill(_kinodataset.tbl_healthtest_type);
			}
			return _kinodataset.tbl_healthtest_type;
		}

		public static bool SaveHealthTestTypes()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_healthtest_type"] != null && _kinodataset.GetChanges().Tables["tbl_healthtest_type"].Rows.Count > 0)
				{
					if (_tbl_healthtest_typeTableAdapter.Update(_kinodataset.tbl_healthtest_type) > 0)
					{
						_kinodataset.tbl_healthtest_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_healthtest_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveHealthTestTypes");
				_kinodataset.tbl_healthtest_type.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_pedigree_healthtestDataTable PedigreeHealthTestsDT()
		{
			Healthtest_typeDT();
			if (_tbl_pedigree_healthtestTableAdapter == null)
			{
				_tbl_pedigree_healthtestTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_healthtestTableAdapter();
				_tbl_pedigree_healthtestTableAdapter.Fill(_kinodataset.tbl_pedigree_healthtest);
			}
			return _kinodataset.tbl_pedigree_healthtest;
		}

		public static bool SavePedigreeHealthTests()
		{
			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree_healthtest"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree_healthtest"].Rows.Count > 0)
				{
					if (_tbl_pedigree_healthtestTableAdapter.Update(_kinodataset.tbl_pedigree_healthtest) > 0)
					{
						_kinodataset.tbl_pedigree_healthtest.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree_healthtest.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SavePedigreeHealthTests");
				_kinodataset.tbl_pedigree_healthtest.RejectChanges();
				return false;
			}
		}
		#endregion

		#region trials
		public static KinoDataSet.tbl_pedigree_trialDataTable PedigreeTrialDT()
		{
			TrialTypeDT();
			if (_tbl_pedigree_trialTableAdapter == null)
			{
				_tbl_pedigree_trialTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_trialTableAdapter();
				_tbl_pedigree_trialTableAdapter.Fill(_kinodataset.tbl_pedigree_trial);
			}
			return _kinodataset.tbl_pedigree_trial;
		}

		public static bool SavePedigreeTrial()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree_trial"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree_trial"].Rows.Count > 0)
				{
					if (_tbl_pedigree_trialTableAdapter.Update(_kinodataset.tbl_pedigree_trial) > 0)
					{
						_kinodataset.tbl_pedigree_trial.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree_trial.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SavePedigreeTrial");
				_kinodataset.tbl_pedigree_trial.RejectChanges();
				return false;
			}
		}


		public static KinoDataSet.tbl_trial_typeDataTable TrialTypeDT()
		{
			if (_tbl_trial_typeTableAdapter == null)
			{
				_tbl_trial_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_trial_typeTableAdapter();
				_tbl_trial_typeTableAdapter.Fill(_kinodataset.tbl_trial_type);
			}
			return _kinodataset.tbl_trial_type;
		}

		public static bool SaveTrialType()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_trial_type"] != null && _kinodataset.GetChanges().Tables["tbl_trial_type"].Rows.Count > 0)
				{
					if (_tbl_trial_typeTableAdapter.Update(_kinodataset.tbl_trial_type) > 0)
					{
						_kinodataset.tbl_trial_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_trial_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveTrialType");
				_kinodataset.tbl_trial_type.RejectChanges();
				return false;
			}
		}
		#endregion


		#region ChampionStatuses
		public static KinoDataSet.tbl_championStatusesDataTable ChampionStatusesDT()
		{
			if (_tbl_championStatusesTableAdapter == null)
			{
				_tbl_championStatusesTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_championStatusesTableAdapter();
				_tbl_championStatusesTableAdapter.Fill(_kinodataset.tbl_championStatuses);
			}
			return _kinodataset.tbl_championStatuses;
		}

		public static bool SaveChampionStatuses()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_championStatuses"] != null && _kinodataset.GetChanges().Tables["tbl_championStatuses"].Rows.Count > 0)
				{
					if (_tbl_championStatusesTableAdapter.Update(_kinodataset.tbl_championStatuses) > 0)
					{
						_kinodataset.tbl_championStatuses.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_championStatuses.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveChampionStatuses");
				_kinodataset.tbl_championStatuses.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_pedigree_champion_statusDataTable PedigreeChampionsDT()
		{
			ChampionStatusesDT();
			if (_tbl_pedigree_champion_statusTableAdapter == null)
			{
				_tbl_pedigree_champion_statusTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_champion_statusTableAdapter();
				_tbl_pedigree_champion_statusTableAdapter.Fill(_kinodataset.tbl_pedigree_champion_status);
			}
			return _kinodataset.tbl_pedigree_champion_status;
		}

		public static bool SavePedigreeChampions()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree_champion_status"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree_champion_status"].Rows.Count > 0)
				{
					if (_tbl_pedigree_champion_statusTableAdapter.Update(_kinodataset.tbl_pedigree_champion_status) > 0)
					{
						_kinodataset.tbl_pedigree_champion_status.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree_champion_status.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SavePedigreeChampions");
				_kinodataset.tbl_pedigree_champion_status.RejectChanges();
				return false;
			}
		}
		#endregion
		public static KinoDataSet KinoDS()
		{
			return _kinodataset;
		}

		public static KinoDataSet.Func_AgeGroupsbyDatesDataTable Func_AgeGroupsbyPedigreeDT(Int32 pedigree_id)
		{
			var query = KinoDS().tbl_pedigree.AsEnumerable().Where(r => r.Field<Int32>("pedigree_id") == pedigree_id);
			foreach (var item in query)
			{
				DataRow idr = item as DataRow;
				DateTime bdate = (DateTime)idr["br_date"];
				return Func_AgeGroupsbyDatesDT(bdate);
			}
			return null;
		}

		public static KinoDataSet.Func_AgeGroupsbyDatesDataTable Func_AgeGroupsbyDatesDT(DateTime bdate)
		{
			string bdatestring = GetSqlTypeDateString(bdate);


			if (_Func_AgeGroupsbyDatesTableAdapter == null)
			{
				_Func_AgeGroupsbyDatesTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.Func_AgeGroupsbyDatesTableAdapter();
			}
			_Func_AgeGroupsbyDatesTableAdapter.Fill(_kinodataset.Func_AgeGroupsbyDates, bdatestring);
			return _kinodataset.Func_AgeGroupsbyDates;
		}

		public static KinoDataSet.Func_DogExhibitionStatusEnumsDataTable Pedigree_exhibitionDT(int ExhID)
		{
			Func_DogExhibitionStatusEnumsDT();
			PersonDT();
			PedigreeDT();
		   
			AgeGroupDT();

			if (_tbl_pedigree_exhibitionTableAdapter == null)
			{
				_tbl_pedigree_exhibitionTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_exhibitionTableAdapter();
			}
			_kinodataset.tbl_pedigree_exhibition.Clear();
			_tbl_pedigree_exhibitionTableAdapter.Fill(_kinodataset.tbl_pedigree_exhibition, ExhID);

			if (_View_DogsListForExhibitionsTableAdapter == null) _View_DogsListForExhibitionsTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.View_DogsListForExhibitionsTableAdapter();
			_kinodataset.View_DogsListForExhibitions.Clear();
			_View_DogsListForExhibitionsTableAdapter.Fill(_kinodataset.View_DogsListForExhibitions);

			return _kinodataset.Func_DogExhibitionStatusEnums;
		}

		public static bool SaveDogExhibitions()
		{
			try
			{
				

				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree_exhibition"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree_exhibition"].Rows.Count > 0)
				{

					//foreach (DataRow dr in _kinodataset.GetChanges().Tables["tbl_pedigree_exhibition"].Rows)
					//{
					//    try
					//    {
					//        if (_tbl_pedigree_exhibitionTableAdapter.Update(dr)==1)
					//        {
					//            dr.AcceptChanges();
					//        }
					//        else
					//        {
					//            dr.RejectChanges();
					//        }
					//    }
					//    catch (Exception ex)
					//    {
					//        MessageBox.Show(ex.Message);
					//        dr.RejectChanges();
					//    }
					//}

					//return true;

					if (_tbl_pedigree_exhibitionTableAdapter.Update(_kinodataset.tbl_pedigree_exhibition) > 0)
					{
						_kinodataset.tbl_pedigree_exhibition.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree_exhibition.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveDogExhibitions");
			   _kinodataset.tbl_pedigree_exhibition.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.Func_DogExhibitionStatusEnumsDataTable Func_DogExhibitionStatusEnumsDT()
		{
			if (_Func_DogExhibitionStatusEnumsTableAdapter == null)
			{
				_Func_DogExhibitionStatusEnumsTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.Func_DogExhibitionStatusEnumsTableAdapter();
				_Func_DogExhibitionStatusEnumsTableAdapter.Fill(_kinodataset.Func_DogExhibitionStatusEnums);
			}
			return _kinodataset.Func_DogExhibitionStatusEnums;
		}

		public static KinoDataSet.View_DogsWithExtendedDescriptionDataTable View_DogsWithExtendedDescription(bool refresh=false)
		{
			if (_View_DogsWithExtendedDescriptionTableAdapter == null)
			{
				_View_DogsWithExtendedDescriptionTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.View_DogsWithExtendedDescriptionTableAdapter();
				_View_DogsWithExtendedDescriptionTableAdapter.Fill(_kinodataset.View_DogsWithExtendedDescription);
			}
			else if (refresh)
			{
				_kinodataset.View_DogsWithExtendedDescription.Clear();
				_View_DogsWithExtendedDescriptionTableAdapter.Fill(_kinodataset.View_DogsWithExtendedDescription);
			}
			return _kinodataset.View_DogsWithExtendedDescription;
		}

		public static KinoDataSet.tbl_next_level_achive_status_templatesDataTable NextLevelTemplateDT()
		{
			if (_tbl_next_level_achive_status_templatesTableAdapter == null)
			{
				_tbl_next_level_achive_status_templatesTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_next_level_achive_status_templatesTableAdapter();
				_tbl_next_level_achive_status_templatesTableAdapter.Fill(_kinodataset.tbl_next_level_achive_status_templates);
			}
			return _kinodataset.tbl_next_level_achive_status_templates;
		}

		public static KinoDataSet.tbl_organizationDataTable OrganisationDT()
		{
			if (_tbl_organizationTableAdapter == null)
			{
				_tbl_organizationTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_organizationTableAdapter();
				_tbl_organizationTableAdapter.Fill(_kinodataset.tbl_organization);
			}
			return _kinodataset.tbl_organization;
		}

		public static KinoDataSet.View_ExhStatusesForReviewListDataTable View_ExhStatusesForReviewListDT()
		{
			if (_View_ExhStatusesForReviewListTableAdapter == null)
			{
				_View_ExhStatusesForReviewListTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.View_ExhStatusesForReviewListTableAdapter();
				_View_ExhStatusesForReviewListTableAdapter.Fill(_kinodataset.View_ExhStatusesForReviewList);
			}
			return _kinodataset.View_ExhStatusesForReviewList;
		}

		public static bool SaveNextLevelTemplate()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_next_level_achive_status_templates"] != null && _kinodataset.GetChanges().Tables["tbl_next_level_achive_status_templates"].Rows.Count > 0)
				{
					if (_tbl_next_level_achive_status_templatesTableAdapter.Update(_kinodataset.tbl_next_level_achive_status_templates) > 0)
					{
						_kinodataset.tbl_next_level_achive_status_templates.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_next_level_achive_status_templates.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveNextLevelTemplate");
				_kinodataset.tbl_next_level_achive_status_templates.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_firstlevel_achive_templatesDataTable FirstLevelTemplateDT()
		{
			if (_tbl_firstlevel_achive_templatesTableAdapter == null)
			{
				_tbl_firstlevel_achive_templatesTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_firstlevel_achive_templatesTableAdapter();
				_tbl_firstlevel_achive_templatesTableAdapter.Fill(_kinodataset.tbl_firstlevel_achive_templates);
			}
			return _kinodataset.tbl_firstlevel_achive_templates;
		}

		public static bool SaveFirstLevelTemplate()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_firstlevel_achive_templates"] != null && _kinodataset.GetChanges().Tables["tbl_firstlevel_achive_templates"].Rows.Count > 0)
				{
					if (_tbl_firstlevel_achive_templatesTableAdapter.Update(_kinodataset.tbl_firstlevel_achive_templates) > 0)
					{
						_kinodataset.tbl_firstlevel_achive_templates.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_firstlevel_achive_templates.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveFirstLevelTemplate");
				_kinodataset.tbl_firstlevel_achive_templates.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_pedigree_documentDataTable PedigreeDocumentsDT()
		{
			if (_tbl_pedigree_documentTableAdapter == null)
			{
				_tbl_pedigree_documentTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigree_documentTableAdapter();
				_tbl_pedigree_documentTableAdapter.Fill(_kinodataset.tbl_pedigree_document);
			}
			return _kinodataset.tbl_pedigree_document;
		}

		public static bool SavePedigreeDocument()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree_document"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree_document"].Rows.Count > 0)
				{
					if (_tbl_pedigree_documentTableAdapter.Update(_kinodataset.tbl_pedigree_document) > 0)
					{
						_kinodataset.tbl_pedigree_document.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree_document.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SavePedigreeDocument");
				_kinodataset.tbl_pedigree_document.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_document_typeDataTable DocumentTypesDT()
		{
			if (_tbl_document_typeTableAdapter == null)
			{
				_tbl_document_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_document_typeTableAdapter();
				_tbl_document_typeTableAdapter.Fill(_kinodataset.tbl_document_type);
			}
			return _kinodataset.tbl_document_type;
		}

		public static bool SaveDocumentTypes()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_document_type"] != null && _kinodataset.GetChanges().Tables["tbl_document_type"].Rows.Count > 0)
				{
					if (_tbl_document_typeTableAdapter.Update(_kinodataset.tbl_document_type) > 0)
					{
						_kinodataset.tbl_document_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_document_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in Savedocument_type");
				_kinodataset.tbl_document_type.RejectChanges();
				return false;
			}
		}

		public static bool SavePedigree()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_pedigree"] != null && _kinodataset.GetChanges().Tables["tbl_pedigree"].Rows.Count > 0)
				{
					if (_tbl_pedigreeTableAdapter.Update(_kinodataset.tbl_pedigree) > 0)
					{
						_kinodataset.tbl_pedigree.AcceptChanges();
						View_DogsWithExtendedDescription(true);
						return true;
					}
					else
					{
						_kinodataset.tbl_pedigree.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SavePedigree");
				_kinodataset.tbl_pedigree.RejectChanges();
				return false;
			}
		}

	 

		public static KinoDataSet.tbl_pedigreeDataTable PedigreeDT()
		{
			try
			{
                
                BreedDT();
				SexDT();
				ColorDT();
				PersonDT();
				DogStatusEnums();
				View_DogsWithExtendedDescription();
				if (_tbl_pedigreeTableAdapter == null)
				{
					_tbl_pedigreeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_pedigreeTableAdapter();
					_tbl_pedigreeTableAdapter.Fill(_kinodataset.tbl_pedigree);
				}
				DocumentTypesDT();
				PedigreeDocumentsDT();

				ChampionStatusesDT();
				PedigreeChampionsDT();
				PedigreeDewormingsDT();
				PedigreeEctoparasitsDT();
				PedigreeHealthTestsDT();
				PedigreeVaccinesDT();
                PedigreeTrialDT();
                return _kinodataset.tbl_pedigree;
			}
			catch (ConstraintException)
			{
				DataRow[] rowErrors = _kinodataset.tbl_pedigree.GetErrors();

				System.Diagnostics.Debug.WriteLine("YourDataTable Errors:"
					+ rowErrors.Length);

				for (int i = 0; i < rowErrors.Length; i++)
				{
					System.Diagnostics.Debug.WriteLine(rowErrors[i].RowError);

					foreach (DataColumn col in rowErrors[i].GetColumnsInError())
					{
						System.Diagnostics.Debug.WriteLine(col.ColumnName
							+ ":" + rowErrors[i].GetColumnError(col));
					}
				}
				return null;
			}

		}

		public static string ConnString()
		{
			CityDT();
			return _tbl_cityTableAdapter.DBConnection.ConnectionString;
		}

		public static KinoDataSet.Func_DogStatusEnumsDataTable DogStatusEnums()
		{
			if (_dogStatusEnumsTableAdapter == null)
			{
				_dogStatusEnumsTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.Func_DogStatusEnumsTableAdapter();
				_dogStatusEnumsTableAdapter.Fill(_kinodataset.Func_DogStatusEnums);
			}
			return _kinodataset.Func_DogStatusEnums;
		}

		public static KinoDataSet.Func_PersonStatusEnumsDataTable PersonStatusEnums()
		{
			if (_func_PersonStatusEnumsTableAdapter == null)
			{
				_func_PersonStatusEnumsTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.Func_PersonStatusEnumsTableAdapter();
				_func_PersonStatusEnumsTableAdapter.Fill(_kinodataset.Func_PersonStatusEnums);
			}
			return _kinodataset.Func_PersonStatusEnums;
		}

		#region ClubPerson
		public static KinoDataSet.tbl_kennel_club_PersonDataTable ClubPersonDT()
		{
			if (_tbl_kennel_club_PersonTableAdapter == null)
			{
				_tbl_kennel_club_PersonTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_kennel_club_PersonTableAdapter();
				_tbl_kennel_club_PersonTableAdapter.Fill(_kinodataset.tbl_kennel_club_Person);
			}
			return _kinodataset.tbl_kennel_club_Person;
		}

		public static bool SaveClubPerson()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_kennel_club_Person"] != null && _kinodataset.GetChanges().Tables["tbl_kennel_club_Person"].Rows.Count > 0)
				{
					if (_tbl_kennel_club_PersonTableAdapter.Update(_kinodataset.tbl_kennel_club_Person) > 0)
					{
						_kinodataset.tbl_kennel_club_Person.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_kennel_club_Person.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in Savekennel_club_Person");
				_kinodataset.tbl_kennel_club_Person.RejectChanges();
				return false;
			}
		}
		#endregion
		public static KinoDataSet.tbl_kennel_breednameDataTable ClubBreedDT()
		{
			if (_tbl_kennel_breednameTableAdapter == null)
			{
				_tbl_kennel_breednameTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_kennel_breednameTableAdapter();
				_tbl_kennel_breednameTableAdapter.Fill(_kinodataset.tbl_kennel_breedname);
			}
			return _kinodataset.tbl_kennel_breedname;
		}

		public static bool SaveClubBreed()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_kennel_breedname"] != null && _kinodataset.GetChanges().Tables["tbl_kennel_breedname"].Rows.Count > 0)
				{
					if (_tbl_kennel_breednameTableAdapter.Update(_kinodataset.tbl_kennel_breedname) > 0)
					{
						_kinodataset.tbl_kennel_breedname.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_kennel_breedname.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in Savekennel_breedname");
				_kinodataset.tbl_kennel_breedname.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_exhibitionDataTable ExhibitionsDT()
		{
			CountryDT();
			CityDT();
			ExhibitionCategoriesDT();
			if (_Func_ExhibitionStatusEnumsTableAdapter == null)
			{
				_Func_ExhibitionStatusEnumsTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.Func_ExhibitionStatusEnumsTableAdapter();
				_Func_ExhibitionStatusEnumsTableAdapter.Fill(_kinodataset.Func_ExhibitionStatusEnums);
			}

			if (_tbl_exhibitionTableAdapter == null)
			{
				_tbl_exhibitionTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_exhibitionTableAdapter();
				_tbl_exhibitionTableAdapter.Fill(_kinodataset.tbl_exhibition);
			}
			//
			return _kinodataset.tbl_exhibition;
		}

		public static bool SaveExhibitions()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_exhibition"] != null && _kinodataset.GetChanges().Tables["tbl_exhibition"].Rows.Count > 0)
				{
					if (_tbl_exhibitionTableAdapter.Update(_kinodataset.tbl_exhibition) > 0)
					{
						_kinodataset.tbl_exhibition.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_exhibition.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveExhibitions");
				_kinodataset.tbl_exhibition.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_kennel_clubDataTable KennelClubsDT()
		{
			CountryDT();
			CityDT();
			PersonDT();
			BreedNameDT();
			ClubPersonDT();
			ClubBreedDT();
			if (_tbl_kennel_clubTableAdapter == null)
			{
				_tbl_kennel_clubTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_kennel_clubTableAdapter();
				_tbl_kennel_clubTableAdapter.Fill(_kinodataset.tbl_kennel_club);
			}
			return _kinodataset.tbl_kennel_club;
		}

		public static bool SaveKennelClubs()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_kennel_club"] != null && _kinodataset.GetChanges().Tables["tbl_kennel_club"].Rows.Count > 0)
				{
					if (_tbl_kennel_clubTableAdapter.Update(_kinodataset.tbl_kennel_club) > 0)
					{
						_kinodataset.tbl_kennel_club.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_kennel_club.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in Savekennel_club");
				_kinodataset.tbl_kennel_club.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_personDataTable PersonDT()
		{
			try
			{
				PersonStatusEnums();
				SexDT();
				CountryDT();
				CityDT();
				NationalityDT();
				BreedDT();

				if (_tbl_personTableAdapter == null)
				{
					_tbl_personTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_personTableAdapter();
					_tbl_personTableAdapter.Fill(_kinodataset.tbl_person);
				}
			}
			catch (ConstraintException)
			{
				DataRow[] rowErrors = _kinodataset.tbl_pedigree.GetErrors();

				System.Diagnostics.Debug.WriteLine("YourDataTable Errors:"
					+ rowErrors.Length);

				for (int i = 0; i < rowErrors.Length; i++)
				{
					System.Diagnostics.Debug.WriteLine(rowErrors[i].RowError);

					foreach (DataColumn col in rowErrors[i].GetColumnsInError())
					{
						System.Diagnostics.Debug.WriteLine(col.ColumnName
							+ ":" + rowErrors[i].GetColumnError(col));
					}
				}
				return null;
			}


			return _kinodataset.tbl_person;
		}

		public static bool SavePerson()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_person"] != null && _kinodataset.GetChanges().Tables["tbl_person"].Rows.Count > 0)
				{
					if (_tbl_personTableAdapter.Update(_kinodataset.tbl_person) > 0)
					{
						_kinodataset.tbl_person.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_person.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in Saveperson");
				_kinodataset.tbl_person.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_sex_typeDataTable SexDT()
		{
			if (_tbl_sex_typeTableAdapter == null)
			{
				_tbl_sex_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_sex_typeTableAdapter();
				_tbl_sex_typeTableAdapter.Fill(_kinodataset.tbl_sex_type);
			}
			return _kinodataset.tbl_sex_type;
		}

		public static bool SaveSex()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_sex_type"] != null && _kinodataset.GetChanges().Tables["tbl_sex_type"].Rows.Count > 0)
				{
					if (_tbl_sex_typeTableAdapter.Update(_kinodataset.tbl_sex_type) > 0)
					{
						_kinodataset.tbl_sex_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_sex_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in Savesex_type");
				_kinodataset.tbl_sex_type.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_breedDataTable BreedDT()
		{

			BreedNameDT();
			Registration_counterDT();
			HairTypeDT();
			SizeTypeDT();
			ColorTypeDT();

			if (_tbl_breedTableAdapter == null)
			{
				_tbl_breedTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_breedTableAdapter();
				_tbl_breedTableAdapter.Fill(_kinodataset.tbl_breed);
			}

			if (_tbl_breed_hairtypeTableAdapter == null)
			{
				_tbl_breed_hairtypeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_hairtypeTableAdapter();
				_tbl_breed_hairtypeTableAdapter.Fill(_kinodataset.tbl_breed_hairtype);
			}

			if (_tbl_breed_colortypeTableAdapter == null)
			{
				_tbl_breed_colortypeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_colortypeTableAdapter();
				_tbl_breed_colortypeTableAdapter.Fill(_kinodataset.tbl_breed_colortype);
			}

			if (_tbl_breed_sizetypeTableAdapter == null)
			{
				_tbl_breed_sizetypeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_sizetypeTableAdapter();
				_tbl_breed_sizetypeTableAdapter.Fill(_kinodataset.tbl_breed_sizetype);
			}



			FillBreedDescriptions();
			//

			return _kinodataset.tbl_breed;
		}

		public static void FillBreedDescriptions(bool refresh=false)
		{
			if (_View_BreedsWithDescriptionsTableAdapter == null)
			{
				_View_BreedsWithDescriptionsTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.View_BreedsWithDescriptionsTableAdapter();
				_View_BreedsWithDescriptionsTableAdapter.Fill(_kinodataset.View_BreedsWithDescriptions);
			}
			else if (refresh)
			{
				_kinodataset.View_BreedsWithDescriptions.Clear();
				_View_BreedsWithDescriptionsTableAdapter.Fill(_kinodataset.View_BreedsWithDescriptions);
			}
		}

		public static bool SaveBreedSizeType()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_breed_sizetype"] != null && _kinodataset.GetChanges().Tables["tbl_breed_sizetype"].Rows.Count > 0)
				{

				   

					if (_tbl_breed_sizetypeTableAdapter.Update(_kinodataset.tbl_breed_sizetype) > 0)
					{
						_kinodataset.tbl_breed_sizetype.AcceptChanges();
						FillBreedDescriptions();
						return true;
					}
					else
					{
						_kinodataset.tbl_breed_sizetype.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveBreedsizeType");
				_kinodataset.tbl_breed_sizetype.RejectChanges();
				return false;
			}
		}

		public static bool SaveBreedColorType()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_breed_colortype"] != null && _kinodataset.GetChanges().Tables["tbl_breed_colortype"].Rows.Count > 0)
				{

					

					if (_tbl_breed_colortypeTableAdapter.Update(_kinodataset.tbl_breed_colortype) > 0)
					{
						_kinodataset.tbl_breed_colortype.AcceptChanges();
						FillBreedDescriptions();
						return true;
					}
					else
					{
						_kinodataset.tbl_breed_colortype.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveBreedColorType");
				_kinodataset.tbl_breed_colortype.RejectChanges();
				return false;
			}
		}

		public static bool SaveBreedHairType()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_breed_hairtype"] != null && _kinodataset.GetChanges().Tables["tbl_breed_hairtype"].Rows.Count > 0)
				{

				   
					if (_tbl_breed_hairtypeTableAdapter.Update(_kinodataset.tbl_breed_hairtype) > 0)
					{
						_kinodataset.tbl_breed_hairtype.AcceptChanges();
						FillBreedDescriptions();
						return true;
					}
					else
					{
						_kinodataset.tbl_breed_hairtype.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveBreedHairType");
				_kinodataset.tbl_breed_hairtype.RejectChanges();
				return false;
			}
		}

		public static bool SaveBreed()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_breed"] != null && _kinodataset.GetChanges().Tables["tbl_breed"].Rows.Count > 0)
				{
					if (_tbl_breedTableAdapter.Update(_kinodataset.tbl_breed) > 0)
					{
						_kinodataset.tbl_breed.AcceptChanges();
						FillBreedDescriptions();
						return true;
					}
					else
					{
						_kinodataset.tbl_breed.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in Savebreed");
				_kinodataset.tbl_breed.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_registration_counterDataTable Registration_counterDT()
		{
			if (_tbl_registration_counterTableAdapter == null)
			{
				_tbl_registration_counterTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_registration_counterTableAdapter();
				_tbl_registration_counterTableAdapter.Fill(_kinodataset.tbl_registration_counter);
			}
			return _kinodataset.tbl_registration_counter;
		}

		public static bool SaveRegistration_counter()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_registration_counter"] != null && _kinodataset.GetChanges().Tables["tbl_registration_counter"].Rows.Count > 0)
				{
					if (_tbl_registration_counterTableAdapter.Update(_kinodataset.tbl_registration_counter) > 0)
					{
						_kinodataset.tbl_registration_counter.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_registration_counter.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in Saveregistration_counter");
				_kinodataset.tbl_registration_counter.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_nationalityDataTable NationalityDT()
		{
			if (_tbl_nationalityTableAdapter == null)
			{
				_tbl_nationalityTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_nationalityTableAdapter();
				_tbl_nationalityTableAdapter.Fill(_kinodataset.tbl_nationality);
			}
			return _kinodataset.tbl_nationality;
		}

		public static bool SaveNationality()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_nationality"] != null && _kinodataset.GetChanges().Tables["tbl_nationality"].Rows.Count > 0)
				{
					if (_tbl_nationalityTableAdapter.Update(_kinodataset.tbl_nationality) > 0)
					{
						_kinodataset.tbl_nationality.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_nationality.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveNationality");
				_kinodataset.tbl_nationality.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_exhib_categoryDataTable ExhibitionCategoriesDT()
		{
			if (_tbl_exhib_categoryTableAdapter == null)
			{
				_tbl_exhib_categoryTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_exhib_categoryTableAdapter();
				_tbl_exhib_categoryTableAdapter.Fill(_kinodataset.tbl_exhib_category);
			}
			return _kinodataset.tbl_exhib_category;
		}

		public static bool SaveExhibitionCategories()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_exhib_category"] != null && _kinodataset.GetChanges().Tables["tbl_exhib_category"].Rows.Count > 0)
				{
					if (_tbl_exhib_categoryTableAdapter.Update(_kinodataset.tbl_exhib_category) > 0)
					{
						_kinodataset.tbl_exhib_category.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_exhib_category.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveExhibitionCategories");
				_kinodataset.tbl_exhib_category.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_exh_statusDataTable ExhibitionStatusesDT()
		{
			if (_tbl_exh_statusTableAdapter == null)
			{
				_tbl_exh_statusTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_exh_statusTableAdapter();
				_tbl_exh_statusTableAdapter.Fill(_kinodataset.tbl_exh_status);
			}
			return _kinodataset.tbl_exh_status;
		}

		public static bool SaveExhibitionStatuses()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_exh_status"] != null && _kinodataset.GetChanges().Tables["tbl_exh_status"].Rows.Count > 0)
				{
					if (_tbl_exh_statusTableAdapter.Update(_kinodataset.tbl_exh_status) > 0)
					{
						_kinodataset.tbl_exh_status.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_exh_status.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveExhibitionStatuses");
				_kinodataset.tbl_exh_status.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_breed_nameDataTable BreedNameDT()
		{
			if (_tbl_breed_nameTableAdapter == null)
			{
				_tbl_breed_nameTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_nameTableAdapter();
				SqlDataAdapter da = GetAdapter(_tbl_breed_nameTableAdapter);
			   // MessageBox.Show(da.UpdateCommand.Connection.ConnectionString);
				_tbl_breed_nameTableAdapter.Fill(_kinodataset.tbl_breed_name);
			}
			return _kinodataset.tbl_breed_name;
		}

		public static bool SaveBreedName()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_breed_name"] != null && _kinodataset.GetChanges().Tables["tbl_breed_name"].Rows.Count > 0)
				{
					if (_tbl_breed_nameTableAdapter.Update(_kinodataset.tbl_breed_name) > 0)
					{
						_kinodataset.tbl_breed_name.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_breed_name.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveBreedName");
				_kinodataset.tbl_breed_name.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_breed_groupDataTable BreedGroupDT()
		{
			if (_tbl_breed_groupTableAdapter == null)
			{
				_tbl_breed_groupTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_breed_groupTableAdapter();
				SqlDataAdapter da = GetAdapter(_tbl_breed_groupTableAdapter);
				//MessageBox.Show(da.UpdateCommand.Connection.ConnectionString);
				_tbl_breed_groupTableAdapter.Fill(_kinodataset.tbl_breed_group);
			}
			return _kinodataset.tbl_breed_group;
		}

		public static bool SaveBreedGroup()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_breed_group"] != null && _kinodataset.GetChanges().Tables["tbl_breed_group"].Rows.Count > 0)
				{
					if (_tbl_breed_groupTableAdapter.Update(_kinodataset.tbl_breed_group) > 0)
					{
						_kinodataset.tbl_breed_group.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_breed_group.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveColorType");
				_kinodataset.tbl_breed_group.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_colorDataTable ColorDT()
		{
			ColorTypeDT();
			if (_tbl_colorTableAdapter == null)
			{
				_tbl_colorTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_colorTableAdapter();
				_tbl_colorTableAdapter.Fill(_kinodataset.tbl_color);
				//_kinodataset.tbl_color_type.RowDeleting += tbl_color_type_RowDeleting;
			}

			if (_tbl_color_type_colorTableAdapter == null)
			{
				_tbl_color_type_colorTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_color_type_colorTableAdapter();
				_tbl_color_type_colorTableAdapter.Fill(_kinodataset.tbl_color_type_color);
			}
			return _kinodataset.tbl_color;
		}

		public static KinoDataSet.tbl_color_typeDataTable ColorTypeDT()
		{
			if (_tbl_color_typeTableAdapter == null)
			{
				_tbl_color_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_color_typeTableAdapter();
				_tbl_color_typeTableAdapter.Fill(_kinodataset.tbl_color_type);
				_kinodataset.tbl_color_type.RowDeleting += tbl_color_type_RowDeleting;
			}
			return _kinodataset.tbl_color_type;
		}

		static void tbl_color_type_RowDeleting(object sender, System.Data.DataRowChangeEventArgs e)
		{
			throw new NotImplementedException();
		}

		public static bool SaveColorType()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_color_type"] != null && _kinodataset.GetChanges().Tables["tbl_color_type"].Rows.Count > 0)
				{
					if (_tbl_color_typeTableAdapter.Update(_kinodataset.tbl_color_type) > 0)
					{
						_kinodataset.tbl_color_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_color_type.RejectChanges();
						return false;
					}

				}
				else
					return true;

					//tbl_color_type_color
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveColorType");
				_kinodataset.tbl_color_type.RejectChanges();
				return false;
			}
		}

		public static bool SaveColor()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_color"] != null && _kinodataset.GetChanges().Tables["tbl_color"].Rows.Count > 0)
				{
					if (_tbl_colorTableAdapter.Update(_kinodataset.tbl_color) > 0)
					{
						_kinodataset.tbl_color.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_color.RejectChanges();
						return false;
					}

				}
				else
					return true;

				//tbl_color_type_color
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveColor");
				_kinodataset.tbl_color.RejectChanges();
				return false;
			}
		}

		public static bool SaveColorTypeColor()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_color_type_color"] != null && _kinodataset.GetChanges().Tables["tbl_color_type_color"].Rows.Count > 0)
				{
					if (_tbl_color_type_colorTableAdapter.Update(_kinodataset.tbl_color_type_color) > 0)
					{
						_kinodataset.tbl_color_type_color.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_color_type_color.RejectChanges();
						return false;
					}

				}
				else
					return true;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveColorTypeColor");
				_kinodataset.tbl_color_type_color.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_size_typeDataTable SizeTypeDT()
		{
			if (_tbl_size_typeTableAdapter == null)
			{
				_tbl_size_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_size_typeTableAdapter();
				_tbl_size_typeTableAdapter.Fill(_kinodataset.tbl_size_type);
			}
			return _kinodataset.tbl_size_type;
		}

		public static bool SaveSizeType()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_size_type"] != null && _kinodataset.GetChanges().Tables["tbl_size_type"].Rows.Count > 0)
				{
					if (_tbl_size_typeTableAdapter.Update(_kinodataset.tbl_size_type) > 0)
					{
						_kinodataset.tbl_size_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_size_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveSizeType");
				_kinodataset.tbl_size_type.RejectChanges();
				return false;
			}
		}

		public static KinoDataSet.tbl_hair_typeDataTable HairTypeDT()
		{
			if (_tbl_hair_typeTableAdapter == null)
			{
				_tbl_hair_typeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_hair_typeTableAdapter();
				_tbl_hair_typeTableAdapter.Fill(_kinodataset.tbl_hair_type);
			}
			return _kinodataset.tbl_hair_type;
		}

		public static bool SaveHairType()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_hair_type"] != null && _kinodataset.GetChanges().Tables["tbl_hair_type"].Rows.Count > 0)
				{
					if (_tbl_hair_typeTableAdapter.Update(_kinodataset.tbl_hair_type) > 0)
					{
						_kinodataset.tbl_hair_type.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_hair_type.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveHairType");
				_kinodataset.tbl_hair_type.RejectChanges();
				return false;
			}
		}

        public static bool SaveCountryCodes()
        {

            try
            {
                if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_country_code"] != null && _kinodataset.GetChanges().Tables["tbl_country_code"].Rows.Count > 0)
                {
                    if (_tbl_country_codeTableAdapter.Update(_kinodataset.tbl_country_code) > 0)
                    {
                        _kinodataset.tbl_country_code.AcceptChanges();
                        return true;
                    }
                    else
                    {
                        _kinodataset.tbl_country_code.RejectChanges();
                        return false;
                    }

                }
                else
                    return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in SaveCountryCodes");
                _kinodataset.tbl_country_code.RejectChanges();
                return false;
            }
        }

        public static KinoDataSet.tbl_country_codeDataTable CountryCodeDT()
		{
			if (_tbl_country_codeTableAdapter == null)
			{
				_tbl_country_codeTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_country_codeTableAdapter();
				_tbl_country_codeTableAdapter.Fill(_kinodataset.tbl_country_code);
			}
			return _kinodataset.tbl_country_code;
		}

		public static KinoDataSet.tbl_cityDataTable CityDT()
		{
			if(_tbl_countryTableAdapter==null)
			{
				_tbl_countryTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_countryTableAdapter();
				_tbl_countryTableAdapter.Fill(_kinodataset.tbl_country);
			}
			if (_tbl_cityTableAdapter == null)
			{
				_tbl_cityTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_cityTableAdapter();
				_tbl_cityTableAdapter.Fill(_kinodataset.tbl_city);
			}
			return _kinodataset.tbl_city;
		}

		public static KinoDataSet.tbl_agegroupDataTable AgeGroupDT()
		{
			if(_tbl_agegroupTableAdapter==null)
			{
				_tbl_agegroupTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_agegroupTableAdapter();
				_tbl_agegroupTableAdapter.Fill(_kinodataset.tbl_agegroup);
			}
			return _kinodataset.tbl_agegroup;
		}

		public static KinoDataSet.tbl_countryDataTable CountryDT()
		{
			
			if(_tbl_countryTableAdapter==null)
			{
				_tbl_countryTableAdapter = new KinoDataLibrary.KinoDataSetTableAdapters.tbl_countryTableAdapter();
				_tbl_countryTableAdapter.Fill(_kinodataset.tbl_country);
			}
			CountryCodeDT();
			return _kinodataset.tbl_country;
		}

		public static bool SaveCity()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_city"] != null && _kinodataset.GetChanges().Tables["tbl_city"].Rows.Count > 0)
				{
					if (_tbl_cityTableAdapter.Update(_kinodataset.tbl_city) > 0)
					{
						_kinodataset.tbl_city.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_city.RejectChanges();
						return false;
					}
						
				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveCity");
				_kinodataset.tbl_city.RejectChanges();
				return false;
			}
		}

		public static bool SaveCountry()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_country"] != null && _kinodataset.GetChanges().Tables["tbl_country"].Rows.Count > 0)
				{
					if (_tbl_countryTableAdapter.Update(_kinodataset.tbl_country) > 0)
					{
						_kinodataset.tbl_country.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_country.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveCountry");
				_kinodataset.tbl_country.RejectChanges();
				return false;
			}
		}

		public static bool SaveAgeGroup()
		{

			try
			{
				if (_kinodataset.HasChanges() && _kinodataset.GetChanges().Tables["tbl_agegroup"] != null && _kinodataset.GetChanges().Tables["tbl_agegroup"].Rows.Count > 0)
				{
					if (_tbl_agegroupTableAdapter.Update(_kinodataset.tbl_agegroup) > 0)
					{
						_kinodataset.tbl_agegroup.AcceptChanges();
						return true;
					}
					else
					{
						_kinodataset.tbl_agegroup.RejectChanges();
						return false;
					}

				}
				else
					return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in SaveAgeGroup");
				_kinodataset.tbl_agegroup.RejectChanges();
				return false;
			}
		}

		//public static KinologyModel GetModel()
		//{
		//    //if(mod==null)
		//    //    mod = new KinologyModel(@"data source=62.212.231.217\\RISK_GEEE_server,9913;initial catalog=Kino;persist security info=True;user id=kino;password=kino");
		//    return mod;
		//}

		public static void Save()
		{
			//mod.SaveChanges();
		}

		public static string GetBreedsByBreedName(Int32 breednameid)
		{
			string sql = "";

			sql = "SELECT DISTINCT dbo.tbl_breed.breed_id from dbo.tbl_breed " +
" WHERE (dbo.tbl_breed.breed_nameID = " + breednameid + ")";
				   

			string res = "";
			SqlCommand SQlcmd;
			SqlConnection connection = null;
			try
			{

				using (connection = new SqlConnection(MyCommonDB.ConnString()))
				{

					SQlcmd = new SqlCommand(sql, connection);
					connection.Open();
					SqlDataReader TempDR = SQlcmd.ExecuteReader();
					if (TempDR.HasRows)
					{
						while (TempDR.Read())
						{
							if (res == "") //Convert('<guid>'
								res = TempDR[0].ToString();
							else
								res += ", " + TempDR[0].ToString();
						}

					}

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in GetBreedsByBreedName");
			}
			finally
			{
				if (connection != null && connection.State == System.Data.ConnectionState.Open)
					connection.Close();
			}
			return res;
		}

		public static string GetBreedsByBreed(Int32 breedid)
		{
			string sql = "";

			sql = "SELECT     breed_id, breed_nameID FROM dbo.tbl_breed WHERE (breed_nameID = (SELECT TOP (1) breed_nameID " + 
" FROM dbo.tbl_breed AS tbl_breed_1  WHERE (breed_id = " + breedid +")))";


			string res = "";
			SqlCommand SQlcmd;
			SqlConnection connection = null;
			try
			{

				using (connection = new SqlConnection(MyCommonDB.ConnString()))
				{

					SQlcmd = new SqlCommand(sql, connection);
					connection.Open();
					SqlDataReader TempDR = SQlcmd.ExecuteReader();
					if (TempDR.HasRows)
					{
						while (TempDR.Read())
						{
							if (res == "") //Convert('<guid>'
								res = TempDR[0].ToString();
							else
								res += ", " + TempDR[0].ToString();
						}

					}

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in GetBreedsByBreed");
			}
			finally
			{
				if (connection != null && connection.State == System.Data.ConnectionState.Open)
					connection.Close();
			}
			return res;
		}


        public static string GetChildsByDog(Int32 dogid)
        {
            string sql = "";

            sql = "SELECT pedigree_id from GetChildsByDog(" + dogid + ")";


            string res = "";
            SqlCommand SQlcmd;
            SqlConnection connection = null;
            try
            {

                using (connection = new SqlConnection(MyCommonDB.ConnString()))
                {

                    SQlcmd = new SqlCommand(sql, connection);
                    connection.Open();
                    SqlDataReader TempDR = SQlcmd.ExecuteReader();
                    if (TempDR.HasRows)
                    {
                        while (TempDR.Read())
                        {
                            if (res == "") //Convert('<guid>'
                                res = TempDR[0].ToString();
                            else
                                res += ", " + TempDR[0].ToString();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " in GetBreedsByBreed");
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
            return res;
        }

        public static string GetChildrensBreedName(Int32 breednameid, BreedChildEnum chltype)
		{

			string sql = "";
			switch (chltype)
			{
				case BreedChildEnum.Hair:
					sql = "SELECT DISTINCT dbo.tbl_breed_hairtype.hair_type_id FROM dbo.tbl_breed INNER JOIN " +
		  " dbo.tbl_breed_name ON dbo.tbl_breed.breed_nameID = dbo.tbl_breed_name.breed_nameID INNER JOIN " +
		  " dbo.tbl_breed_hairtype ON dbo.tbl_breed.breed_id = dbo.tbl_breed_hairtype.breed_id " +
" WHERE (dbo.tbl_breed_name.breed_nameID = " + breednameid + ")";
					break;
				case BreedChildEnum.Color:
					sql = "SELECT DISTINCT dbo.tbl_breed_colortype.color_type_id FROM dbo.tbl_breed INNER JOIN " +
		  " dbo.tbl_breed_name ON dbo.tbl_breed.breed_nameID = dbo.tbl_breed_name.breed_nameID INNER JOIN " +
		  " dbo.tbl_breed_colortype ON dbo.tbl_breed.breed_id = dbo.tbl_breed_colortype.breed_id " +
" WHERE (dbo.tbl_breed_name.breed_nameID = " + breednameid + ")";
					break;
				case BreedChildEnum.Size:
					sql = "SELECT DISTINCT dbo.tbl_breed_sizetype.size_type_id FROM dbo.tbl_breed INNER JOIN " +
		  " dbo.tbl_breed_name ON dbo.tbl_breed.breed_nameID = dbo.tbl_breed_name.breed_nameID INNER JOIN " +
		  " dbo.tbl_breed_sizetype ON dbo.tbl_breed.breed_id = dbo.tbl_breed_sizetype.breed_id " +
" WHERE (dbo.tbl_breed_name.breed_nameID = " + breednameid + ")";
					break;
				default:
					break;
			}


			string res = "";
			SqlCommand SQlcmd;
			SqlConnection connection = null;
			try
			{

				using (connection = new SqlConnection(MyCommonDB.ConnString()))
				{

					SQlcmd = new SqlCommand(sql, connection);
					connection.Open();
					SqlDataReader TempDR = SQlcmd.ExecuteReader();
					if (TempDR.HasRows)
					{
						while (TempDR.Read())
						{
							if (res == "") //Convert('<guid>'
								res =  TempDR[0].ToString() ;
							else
								res += ", " + TempDR[0].ToString();
						}

					}

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + " in GetHairTypesByBreedName");
			}
			finally
			{
				if (connection != null && connection.State == System.Data.ConnectionState.Open)
					connection.Close();
			}
			return res;
		}

		public static T FindFirstElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
		{
			var count = VisualTreeHelper.GetChildrenCount(parentElement);
			if (count == 0)
				return null;

			for (int i = 0; i < count; i++)
			{
				var child = VisualTreeHelper.GetChild(parentElement, i);

				if (child != null && child is T)
				{
					return (T)child;
				}
				else
				{
					var result = FindFirstElementInVisualTree<T>(child);
					if (result != null)
						return result;

				}
			}
			return null;
		}

		public static DependencyObject GetParentDependencyObjectFromVisualTree(DependencyObject startObject, Type type)
		{
			//Walk the visual tree to get the parent of this control
			DependencyObject parent = startObject;
			while (parent != null)
			{
				if (type.IsInstanceOfType(parent))
					break;
				else
					parent = VisualTreeHelper.GetParent(parent);
			}
			return parent;
		}

		public static T FindVisualChild<T>(DependencyObject parent, string childName) where T : DependencyObject
		{
			if (parent == null)
				return null;

			T foundChild = null;

			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);
				T childType = child as T;
				if (childType == null)
				{
					foundChild = FindVisualChild<T>(child, childName);

					if (foundChild != null) break;
				}
				else if (!string.IsNullOrEmpty(childName))
				{
					var frameworkElement = child as FrameworkElement;
					if (frameworkElement != null && frameworkElement.Name == childName)
					{
						foundChild = (T)child;
						break;
					}
				}
				else
				{
					foundChild = (T)child;
					break;
				}
			}

			return foundChild;
		}

		public static childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is childItem)
					return (childItem)child;
				else
				{
					childItem childOfChild = FindVisualChild<childItem>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}
			return null;
		}

		public static int GetExhibitionRegistrationCount(Int32 exhibid)
		{
			Int32 newProdID = 0;
			string sql =
				"select   dbo.Func_GetExhibitionRegistrationCount(" + exhibid + ") as d";
			using (SqlConnection conn = new SqlConnection(ConnString()))
			{
				SqlCommand cmd = new SqlCommand(sql, conn);
				try
				{
					conn.Open();
					newProdID = (Int32)cmd.ExecuteScalar();
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
			return (int)newProdID;
		}

		public static string GetSqlTypeDateString(DateTime datearg)
		{
			string Daystr = null;
			string MonthStr = null;
			string YearStr = null;

			Daystr = datearg.Day.ToString();
			MonthStr = datearg.Month.ToString();
			YearStr = datearg.Year.ToString();
			return MonthStr + "/" + Daystr + "/" + YearStr;
		}

		public static SqlDataAdapter GetAdapter(object tableAdapter)
		{
			Type tableAdapterType = tableAdapter.GetType();
			SqlDataAdapter adapter = (SqlDataAdapter)tableAdapterType.GetProperty("Adapter",
				   BindingFlags.Instance | BindingFlags.NonPublic).GetValue(tableAdapter, null);
			return adapter;
		}
	}

	public static class Common
	{
		public static void SavePhotoToDataRow(ref DataRow curdr, string fieldname, string filepath="")
		{


			try 
			{

				Stream myStream = default(Stream);
				System.Windows.Forms.OpenFileDialog controlex = new System.Windows.Forms.OpenFileDialog();
				byte[] blobs = null;
				FileStream fsBLOBFile;
				string strBLOBFilePath = null;
				if (filepath == "")
				{
					if (controlex.ShowDialog() == System.Windows.Forms.DialogResult.OK)  strBLOBFilePath = controlex.FileName;
				}
				else strBLOBFilePath = filepath;

				fsBLOBFile = new FileStream(strBLOBFilePath, FileMode.Open, FileAccess.Read);

				if ((fsBLOBFile != null)) {
					// Insert code to read the stream here.
					//myStream.Close()
					byte[] bytBLOBData = new byte[fsBLOBFile.Length];
					fsBLOBFile.Read(bytBLOBData, 0, bytBLOBData.Length);
					fsBLOBFile.Close();
					blobs = bytBLOBData;
				}

				if ((blobs != null)) 
				{
					curdr[fieldname] = blobs;
					if (curdr.Table.Columns["file_name"]!=null)
						curdr["file_name"] = controlex.SafeFileName;
				}

			} 
			catch (System.Exception fillException) 
			{
				MessageBox.Show(fillException.Message + " in SavePhotoToDataRow");
			}

		}

		public static string CurrentPath()
		{
			string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
			string str = appPath.Replace(@"file:\", "");
			return str;
		}

		public static void OpenFileFromDataRow(DataRow curdr, string fieldname, string bytefieldname)
		{
			try
			{
				byte[] blobs = curdr[bytefieldname] as byte[];
				string strBLOBFilePath = null;
				string path = Path.GetTempPath();
				strBLOBFilePath = curdr[fieldname].ToString();
				if ((blobs != null))
				{
					File.WriteAllBytes(path + @"\" + strBLOBFilePath, blobs);
					Process.Start(path + @"\" + strBLOBFilePath);
				}
			}
			catch (System.Exception fillException)
			{
				MessageBox.Show(fillException.Message + " in OpenFileFromDataRow");
			}

		}
	}

	public class ModifiedRowStyle : StyleSelector
	{
		public override Style SelectStyle(object item, DependencyObject container)
		{
			if (item is DataRowView)
			{
				DataRow dr = (item as DataRowView).Row;
				if(dr.RowState== DataRowState.Unchanged)
					return NotChangedStyle;
				else
					return ChangedStyle;
			}

			if ((item as DataRow)!=null)
			{
				DataRow dr = item as DataRow;
				if (dr.RowState == DataRowState.Unchanged)
					return NotChangedStyle;
				else
					return ChangedStyle;
			}
			return null;
		}
		public Style NotChangedStyle { get; set; }
		public Style ChangedStyle { get; set; }
	}

}

