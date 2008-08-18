using FluentNHibernate.Cfg;
using NHibernate.Cfg;
using NUnit.Framework;

namespace FluentNHibernate.Testing.Cfg
{
	[TestFixture]
	public class PersistenceConfigurationTester
	{
		#region Test Setup
		private ConfigTester _config;
		private Configuration _nhibConfig;

		[SetUp]
		public void SetUp()
		{
			_nhibConfig = null;
			_config = new ConfigTester();
		}

		public string ValueOf(string key)
		{
			if( _nhibConfig == null )
			{
				_nhibConfig = new Configuration();
				_config.ConfigureProperties(_nhibConfig);
			}

			return _nhibConfig.GetProperty(key);
		}
		#endregion

		[Test]
		public void Setting_raw_values_should_populate_dictionary()
		{
			_config.Raw("TESTKEY", "TESTVALUE");

			ValueOf("TESTKEY").ShouldEqual("TESTVALUE");
		}

		[Test]
		public void Dialect_should_set_both_old_and_new_dialect_keys()
		{
			_config.Dialect("foo");

			ValueOf("dialect").ShouldEqual("foo");
			ValueOf("hibernate.dialect").ShouldEqual("foo");
		}

		[Test]
		public void Show_Sql_should_set_value_to_const_true()
		{
			_config.ShowSql();
			ValueOf("show_sql").ShouldEqual("true");
		}

		[Test]
		public void UseOuterJoin_should_set_value_to_const_true()
		{
			_config.UseOuterJoin();
			ValueOf("use_outer_join").ShouldEqual("true");
		}

        [Test]
        public void Use_Reflection_Optimizer_should_set_value_to_const_true()
        {
            _config.UseReflectionOptimizer();
            ValueOf("use_reflection_optimizer").ShouldEqual("true");

        }

	    [Test]
        public void Max_Fetch_Depth_should_set_property_value()
	    {
	        _config.MaxFetchDepth(2);
            ValueOf("max_fetch_depth").ShouldEqual("2");
	    }

	    [Test]
	    public void DoNot_ShowSql_should_set_the_property_to_const_false()
	    {
	        _config.DoNot.ShowSql();
	        ValueOf("show_sql").ShouldEqual("false");
            
	    }

	    [Test]
	    public void DoNot_Should_only_affect_next_property()
	    {
	        _config.DoNot.ShowSql().UseReflectionOptimizer();
            ValueOf("show_sql").ShouldEqual("false");
            ValueOf("use_reflection_optimizer").ShouldEqual("true");
	    }
        
        [Test]
	    public void Repeated_DoNot_Calls()
	    {
	        _config.DoNot.ShowSql().DoNot.UseReflectionOptimizer();
            ValueOf("show_sql").ShouldEqual("false");
            ValueOf("use_reflection_optimizer").ShouldEqual("false");
	    }

	    [Test]
	    public void DoNot_UseReflectionOptimizer_should_set_the_property_to_const_false()
	    {
	        _config.DoNot.UseReflectionOptimizer();
	        ValueOf("use_reflection_optimizer").ShouldEqual("false");
	    }

	    [Test]
	    public void DoNot_UseOuterJoin_should_set_the_property_to_const_false()
	    {
	        _config.DoNot.UseOuterJoin();
	        ValueOf("use_outer_join").ShouldEqual("false");
	    }

        public class ConfigTester : PersistenceConfiguration<ConfigTester>
		{
		}
	}
}
