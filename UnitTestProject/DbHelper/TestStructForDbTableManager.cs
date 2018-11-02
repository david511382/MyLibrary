using MyDbHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Tests
{
    class ErrTestStructForDbTableManager : DbTable
    {
        public int id { get; set; }
        public string lotterycode { get; set; }
        public string lotteryopen_no { get; set; }
        public string issue_no { get; set; }
        public DateTime open_time { get; set; }
        public int state { get; set; }
        public int is_manual { get; set; }
        public DateTime add_time { get; set; }

        public string GetTableName()
        {
            return "dt_lottery_open";
        }
    }

    class TestStructForDbTableManager : DbTable
    {
        [NotInserAble]
        public int id { get; set; }
        public string lotterycode { get; set; }
        public string lotteryopen_no { get; set; }
        public string issue_no { get; set; }
        public DateTime open_time { get; set; }
        public int state { get; set; }
        public int is_manual { get; set; }
        public DateTime add_time { get; set; }

        public string GetTableName()
        {
            return "dt_lottery_open";
        }
    }
}
