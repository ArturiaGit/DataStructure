namespace ListTests
{
    [TestClass]
    public class ListTests
    {
#pragma warning disable 8618
        private List.List<string> _list;
#pragma warning restore 8618

        [TestInitialize]
        public void InitList()
        {
            IEnumerable<string> enumerable = ["1", "2", "3", "4"];
            _list = new(enumerable);
        }

        [TestMethod]
        public void Constructor_ParameterIsNull_ThrowArgumentNullException()
        {
            List<string> valS = null!;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                List.List<string> list = new(valS);
            });
        }

        [TestMethod]
        public void Constructor_ParameterValid_ConvertICollection()
        {
            IEnumerable<string> enumerable = ["1", "2", "3", "4"];
            List.List<string> list = new(enumerable);

            int exceptedCount = 4;
            int actualCount = list.Length;
            Assert.AreEqual(exceptedCount, actualCount);
        }

        [TestMethod]
        public void Add_WithIndex_ParameterIsNull_ThrowArgumentNullException()
        {
            string? val = null;

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                List.List<string> list = [val!];
            });
        }

        [TestMethod]
        public void Add_WithIndex_ParameterValid_EmptyList_Succeed()
        {
            string addVal = "1";
            List.List<string> list = [addVal];

            int exceptedCount = 1;
            int actualCount = list.Length;
            Assert.AreEqual(exceptedCount,actualCount);

            string actualVal = list.GetVal(0);
            Assert.AreEqual(addVal,actualVal);
        }

        [TestMethod]
        public void Add_WithIndex_ParameterValid_Succeed()
        {
            _list.Add("5");
            _list.Add("6");

            int exceptedCount = 6;
            int actualCount = _list.Length;
            Assert.AreEqual(exceptedCount,actualCount);

            string exceptedVal = "5";
            string actualVal = _list.GetVal(4);
            Assert.AreEqual(exceptedVal, actualVal);

            exceptedVal = "6";
            actualVal = _list.GetVal(5);
            Assert.AreEqual(exceptedVal, actualVal);
        }

        [TestMethod]
        public void Add_WithIndexAndVal_IndexOutRange_ThrowArgumentOutOfRangeException()
        {
            int addIndex = 4;
            string addVal = "6";
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _list.Add(addIndex, addVal);
            });
        }

        [TestMethod]
        public void Add_WithIndexAndVal_IndexUnderRange_ThrowArgumentOutOfRangeException()
        {
            int addIndex = -1;
            string addVal = "6";
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _list.Add(addIndex, addVal);
            });
        }

        [TestMethod]
        public void Add_WithIndexAndVal_ValIsNull_ThrowArgumentNullException()
        {
            int addIndex = 3;
            string? addVal = null;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _list.Add(addIndex, addVal!);
            });
        }

        [TestMethod]
        public void Add_WithIndexAndVal_ParameterValid_ReturnExceptedResult()
        {
            #region 第一次添加：值为6（线性表开头）
            string addVal = "6";
            int addIndex = 0;
            _list.Add(addIndex, addVal);

            string exceptedVal = addVal;
            string actualVal = _list.GetVal(0);
            Assert.AreEqual(exceptedVal, actualVal);

            exceptedVal = "1";
            actualVal = _list.GetVal(1);
            Assert.AreEqual(exceptedVal, actualVal);
            #endregion

            #region 第二次添加：值为7（线性表末尾）
            addVal = "7";
            addIndex = 4;
            _list.Add(addIndex, addVal);

            exceptedVal = addVal;
            actualVal = _list.GetVal(4);
            Assert.AreEqual(exceptedVal, actualVal);

            exceptedVal = "4";
            actualVal = _list.GetVal(5);
            Assert.AreEqual(exceptedVal, actualVal);
            #endregion

            int exceptedCount = 6;
            int actualCount = _list.Length;
            Assert.AreEqual(exceptedCount, actualCount);
        }

        [TestMethod]
        public void AddRange_ParameterIsNull_ThrowArgumentNullException()
        {
            IEnumerable<string>? enumerable = null;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _list.AddRange(enumerable!);
            });
        }

        [TestMethod]
        public void AddRange_ParameterValid_ReturnExceptedResult()
        {
            IEnumerable<string> addEnumerable = ["5", "6", "7", "8"];
            _list.AddRange(addEnumerable);

            string exceptedVal = "5";
            string actualVal = _list.GetVal(4);
            Assert.AreEqual(exceptedVal, actualVal);

            int exceptedCount = 8;
            int actualCount = _list.Length;
            Assert.AreEqual(exceptedCount, actualCount);
        }

        [TestMethod]
        public void RemoveAt_IndexUnderZero_ThrowArgumentOutOfRangeException()
        {
            int removeIndex = -1;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _list.RemoveAt(removeIndex);
            });
        }

        [TestMethod]
        public void RemoveAt_IndexOutRange_ThrowArgumentOutOfRangeException()
        {
            int removeIndex = 4;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                _list.RemoveAt(removeIndex);
            });
        }

        [TestMethod]
        public void RemoveAt_ParameterValid_ReturnExceptedResult()
        {
            //删除线性表开头元素
            int removeIndex = 0;
            _list.RemoveAt(removeIndex);

            string exceptedVal = "2";
            string actualVal = _list.GetVal(0);
            Assert.AreEqual(exceptedVal, actualVal);

            int exceptedCount = 3;
            int actualCount = _list.Length;
            Assert.AreEqual(exceptedCount, actualCount);

            //删除线性表末尾元素
            removeIndex = 2;
            _list.RemoveAt(removeIndex);

            exceptedVal = "3";
            actualVal = _list.GetVal(1);
            Assert.AreEqual(exceptedVal, actualVal);

            exceptedCount = 2;
            actualCount = _list.Length;
            Assert.AreEqual(exceptedCount, actualCount);
        }

        [TestMethod]
        public void Contains_ValIsNull_ThrowArgumentNullException()
        {
            string? val = null;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _list.Contains(val!);
            });
        }

        [TestMethod]
        public void Contains_ValNotExist_ReturnExceptedResult()
        {
            string val = "5";

            int exceptedIndex = -1;
            int actualIndex = _list.Contains(val);
            Assert.AreEqual(exceptedIndex, actualIndex);
        }

        [TestMethod]
        public void Contains_ParameterValid_ReturnExceptedResult()
        {
            string val = "1";

            int exceptedIndex = 0;
            int actualIndex = _list.Contains(val);
            Assert.AreEqual(exceptedIndex, actualIndex);

            val = "4";

            exceptedIndex = 3;
            actualIndex = _list.Contains(val);
            Assert.AreEqual(exceptedIndex, actualIndex);
        }

        [TestMethod]
        public void Remove_ParameterIsNull_ThrowArgumentNullException()
        {
            string? val = null;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _list.Remove(val!);
            });
        }

        [TestMethod]
        public void Remove_ParameterNotExist_ThrowInvalidOperationException()
        {
            string val = "5";
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                _list.Remove(val);
            });
        }

        [TestMethod]
        public void Remove_ParameterValid_ReturnExceptedResult()
        {
            string removeVal = "1";
            _list.Remove(removeVal);

            int exceptedCount = 3;
            int actualCount = _list.Length;
            Assert.AreEqual(exceptedCount, actualCount);

            string exceptedVal = "2";
            string actualVal = _list.GetVal(0);
            Assert.AreEqual(exceptedVal, actualVal);
        }

        [TestMethod]
        public void Update_ParameterValid_ReturnExceptedResult()
        {
            int updateIndex = 3;
            string updateVal = "6";

            _list.Update(updateIndex, updateVal);

            int exceptedCount = 4;
            int actualCount = _list.Length;
            Assert.AreEqual(exceptedCount, actualCount);

            string exceptedVal = updateVal;
            string actualVal = _list.GetVal(3);
            Assert.AreEqual(exceptedVal, actualVal);
        }
    }

}