using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{

    //Subir para o gitHub antes de editar esse teste. IMPORTANTE. 


    //Decorators
    [TestClass]
    public class FIleProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\Users\Mariana Machado\Desktop\TESTE.txt";
        private string _GoodFileName;
        public TestContext TestContext { get; set; }   // Criando o Test Context


        #region Teste Initialize and Cleanup
        //Teste que vai rodar antes de todos os outros:
        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName == "FileNameDoesExist")
            {
                if (string.IsNullOrEmpty(_GoodFileName))
                {
                    setGoodFileName();
                    TestContext.WriteLine($"Creating File:{_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some text");
                    TestContext.WriteLine("Testing File");
                }

            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (TestContext.TestName == "FileNameDoesExist")
            {
                TestContext.WriteLine("Deleting File");
                File.Delete(_GoodFileName);

            }
        }




        #endregion


        #region Teste AppConfig e arquivo txt


        public void setGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];

            //Condicional do replace do do AppPath -
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        public void fileNameTestsGoodFile()
        {

            FileProcess teste1 = new FileProcess();
            bool anserCall;
            //Mudar o que contém dentro do arquivo caso ele exista. 
            setGoodFileName();
            TestContext.WriteLine($"Creating File:{_GoodFileName}");
            File.AppendAllText(_GoodFileName, "Some text");
            TestContext.WriteLine("Testing File");
            anserCall = teste1.FileExists(_GoodFileName);
            TestContext.WriteLine("Deleting File");
            File.Delete(_GoodFileName);

            Assert.IsTrue(anserCall);
        }

        #endregion

        #region Deploymentltem

        private const string FILE_NAME = @"FileToDeploy.txt";
        [TestMethod]
        [Owner("MarianaM")]
        [DeploymentItem(FILE_NAME)]
        public void FileDoesExistDeploy()
        {
            FileProcess fpTwo = new FileProcess();
            bool fromCallTwo;
            string fileName;


            fileName = $@"{ TestContext.DeploymentDirectory}\{FILE_NAME}";

            TestContext.WriteLine($"Checkin Files {fileName}");
            fromCallTwo = fpTwo.FileExists(fileName);

            Assert.IsTrue(fromCallTwo);
        }
 
        #endregion
        
        
        #region Testes iniciais existência de arquivos
        [Timeout(2000)]
        public void simulateTimeout()
        {

            System.Threading.Thread.Sleep(3000);
        }

        //Se o arquivo existe
        [TestMethod]
        [Description("Teste que faz o check da existência do arquivo .")]
        [Owner("Mariana M")]
        [Ignore]
        [Priority(0)]
        [TestCategory("NoException")]
        public void FileNameDoesExists()
        {
            //Inicialização das variáveis A - Arrange
            FileProcess fp = new FileProcess();
            bool fromCall;

            //Invocando o método criado - Act 
            fromCall = fp.FileExists(@"C:\Users\Mariana Machado\Desktop\AulasAluraSamu\ERROSQUEPODEMOCORRER.txt");

            //Validação da funcionalidade - Assert
            Assert.IsTrue(fromCall);
        }

        //Se o arquivo NÃO existir.
        [TestMethod]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);
            Assert.IsFalse(fromCall);

        }
        //Se o arquivo é vazio ou nulo 


        [TestMethod]
        //Técnica da definição do método 
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrEmpty_ThrowsArgumentsNullException()
        {
            FileProcess fp = new FileProcess();
            fp.FileExists("");
        }

        [TestMethod]
        //Técnica do try catch
        [TestCategory("Exception")]
        public void FileNameNullOrEmpty_ThrowsArgumentsNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();
            try
            {
                fp.FileExists("");
            }
            catch (ArgumentException)
            {
                //The test was a sucess 
                return;
            }

            Assert.Fail("Fail Exepected.");
        }
    }
    #endregion

    #region Class Assert



    #endregion







}
