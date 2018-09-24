using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TodoApi;
using TodoApi.Controllers;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests
{
    public class TodoApiTests
    {
        // List of notes used as a fake database
        private List<Note> GetMockDatabase()
        {
            return new List<Note> {
                new Note
                {
                    NoteId = 1,
                    Title = "Things to do",
                    CheckList = new List<CheckListItem> {
                        new CheckListItem {
                            Id = 1,
                            Text = "Submit assignment before 6:00PM",
                            NoteId = 1
                        }
                    },
                    Labels = new List<Label> {
                        new Label {
                            Id = 1,
                            Name = "ASP.NETCore"
                        }
                    }
                },
                new Note {
                    NoteId = 2,
                    title = "Trial",
                    CheckList = new List<CheckListItem> {
                        new CheckListItem {
                            Id = 2,
                            Text = "Try Xunit",
                            NoteId = 2
                        }
                    },
                    Labels = new List<Label> {
                        new Label {
                            Id = 2,
                            Name = "Trial"
                        }
                    }
                }
            };
        }

        [Fact]
        public void GetAll_Positive_ListWithEntries()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            datarepo.Setup(d => d.GetAllNotes()).Returns(notes);
            TodoController todoController = new TodoController(datarepo.Object);

            var actionResult = todoController.Get();

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Note>;
            Assert.NotNull(model);

            Assert.Equal(notes.Count, model.Count);
        }

        [Fact]
        public void GetAll_Negative_EmptyList()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = new List<Note>();

            datarepo.Setup(d => d.GetAllNotes()).Returns(notes);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Get();

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Note>;
            Assert.NotNull(model);

            Assert.Equal(notes.Count, model.Count);
        }

        [Fact]
        public void GetAll_Negative_DatabaseError()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = null;
            datarepo.Setup(d => d.GetAllNotes()).Returns(notes);
            TodoController todoController = new TodoController(datarepo.Object);
            var result = todoController.Get();
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetById_Positive_ReturnsNoteWithId1()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            int id = 1;
            datarepo.Setup(d => d.GetNote(id)).Returns(notes.Find(n => n.NoteId == id));
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Get(id);

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as Note;
            Assert.NotNull(model);

            Assert.Equal(id, model.NoteId);
        }

        [Fact]
        public void GetById_Negative_ReturnsNullNotFound()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            int id = 3;
            datarepo.Setup(d => d.GetNote(id)).Returns(notes.Find(n => n.NoteId == id));
            TodoController todoController = new TodoController(datarepo.Object);
            var result = todoController.Get(id);
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetByTitle_Positive_ReturnsNoteWithTitle()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            int expected = 1;
            string type = Constant.Type.Title;
            string text = "Trial";
            datarepo.Setup(d => d.GetNote(text, type)).Returns(notes.FindAll(n => n.Title == text));
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Get(text, type);

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as List<Note>;
            Assert.NotNull(model);

            Assert.Equal(expected, model.Count);
        }

        [Fact]
        public void GetByTitle_Negative_ReturnsNotFound()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            string type = Constant.Type.Title;
            string text = "NonExistentTitle";
            datarepo.Setup(d => d.GetNote(text, type)).Returns(notes.FindAll(n => n.Title == text));
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Get(text, type);

            var nfObjectResult = actionResult as NotFoundObjectResult;
            Assert.NotNull(nfObjectResult);
        }

        [Fact]
        public void GetByTitle_Negative_ReturnsBadRequest()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = null;
            string type = "NonExistentType";
            string text = "NonExistentTitle";
            datarepo.Setup(d => d.GetNote(text, type)).Returns(notes);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Get(text, type);

            var brObjectResult = actionResult as BadRequestObjectResult;
            Assert.NotNull(brObjectResult);
        }

        [Fact]
        public void PostById_Positive_ReturnsCreated()
        {
            var datarepo = new Mock<IDataRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            datarepo.Setup(d => d.PostNote(note)).Returns(true);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Post(note);

            var crObjectResult = actionResult as CreatedResult;
            Assert.NotNull(crObjectResult);

            var model = crObjectResult.Value as Note;
            Assert.Equal(note.NoteId, model.NoteId);
        }

        [Fact]
        public void PostById_Negative_ReturnsBadRequest()
        {
            var datarepo = new Mock<IDataRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            datarepo.Setup(d => d.PostNote(note)).Returns(false);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Post(note);

            var brObjectResult = actionResult as BadRequestObjectResult;
            Assert.NotNull(brObjectResult);
        }

        [Fact]
        public void PutById_Positive_ReturnsCreated()
        {
            var datarepo = new Mock<IDataRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            int id = (int)note.NoteId;
            datarepo.Setup(d => d.PutNote(id, note)).Returns(true);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Put(id, note);

            var crObjectResult = actionResult as CreatedResult;
            Assert.NotNull(crObjectResult);

            var model = crObjectResult.Value as Note;
            Assert.Equal(id, model.NoteId);
        }

        [Fact]
        public void PutById_Negative_ReturnsNotFoundt()
        {
            var datarepo = new Mock<IDataRepo>();
            Note note = new Note
            {
                NoteId = 4,
                Title = "Testing Post"
            };
            int id = (int)note.NoteId;
            datarepo.Setup(d => d.PostNote(note)).Returns(false);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Put(id, note);

            var nfObjectResult = actionResult as NotFoundObjectResult;
            Assert.NotNull(nfObjectResult);
        }

        [Fact]
        public void DeleteById_Positive_ReturnsCreated()
        {
            var datarepo = new Mock<IDataRepo>();
            int id = 1;
            datarepo.Setup(d => d.DeleteNote(id)).Returns(true);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Delete(id);

            var okObjectResult = actionResult as OkObjectResult;
            Assert.NotNull(okObjectResult);
        }

        [Fact]
        public void DeleteById_Negative_ReturnsNotFoundt()
        {
            var datarepo = new Mock<IDataRepo>();
            int id = 5;
            datarepo.Setup(d => d.DeleteNote(id)).Returns(false);
            TodoController todoController = new TodoController(datarepo.Object);
            var actionResult = todoController.Delete(id);

            var nfObjectResult = actionResult as NotFoundObjectResult;
            Assert.NotNull(nfObjectResult);
        }
    }
}