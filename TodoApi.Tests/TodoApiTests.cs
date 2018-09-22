using System;
using System.Linq;
using Xunit;
using Moq;
using TodoApi.Models;
using TodoApi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Tests
{
    public class TodoApiTests
    {

        private List<Note> GetMockDatabase(){
            return new List<Note>{
                new Note{
                    NoteId = 1,
                    Title = "Things to do",
                    CheckList = new List<CheckListItem>{
                        new CheckListItem{
                            Id = 1,
                            Text = "Submit assignment before 6:00PM",
                            NoteId = 1
                        }
                    },
                    Labels = new List<Label>{
                        new Label{
                            Id = 1,
                            Name = "ASP.NETCore"
                        }
                    }
                },
                new Note{
                    NoteId = 2,
                    Title = "Trial",
                    CheckList = new List<CheckListItem>{
                        new CheckListItem{
                            Id = 2,
                            Text = "Try Xunit",
                            NoteId = 2
                        }
                    },
                    Labels = new List<Label>{
                        new Label{
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
            var result = todoController.Get();
            Assert.NotNull(result);
            //Assert.Equal(notes.Count ,new List<Note>(result.Value).Count );
        }

        
        // [Fact]
        // public void GetAll_Negative_EmptyList(){
        //     var datarepo = new Mock<IDataRepo>();
        //     List<Note> notes = new List<Note>();
        //     datarepo.Setup(d => d.GetAllNotes()).Returns(notes);
        //     TodoController todoController = new TodoController(datarepo.Object);
        //     var result = todoController.Get();
        //     Assert.NotNull(result);
        //     Assert.Equal(0, result.Value.Count);
        // }
        // [Fact]
        // public void GetById_Positive_ReturnsNoteWithId1(){
        //     var datarepo = new Mock<IDataRepo>();
        //     List<Note> notes = GetMockDatabase();
        //     int id = 1;
        //     datarepo.Setup(d => d.GetNote(id)).Returns(notes.Find(n => n.NoteId == id));
        //     TodoController todoController = new TodoController(datarepo.Object);
        //     var result = todoController.Get(id);
        //     Assert.NotNull(result);
        //     Assert.Equal(id, result.Value.NoteId);
        // }
        [Fact]
        public void GetById_Negative_ReturnsNullNotFound()
        {
            var datarepo = new Mock<IDataRepo>();
            List<Note> notes = GetMockDatabase();
            int id = 3;
            datarepo.Setup(d => d.GetNote(id)).Returns(notes.Find(n => n.NoteId == id));
            TodoController todoController = new TodoController(datarepo.Object);
            var result = todoController.Get(id);
            Assert.Null(result.Value);
        }


    }
}
