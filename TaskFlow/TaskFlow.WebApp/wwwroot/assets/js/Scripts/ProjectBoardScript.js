 const project_app_url = "https://localhost:7006"


 document.addEventListener('DOMContentLoaded', fetchSectionsAndTasks);
 //what does a event listener do
 //It waits for the DOM to be fully loaded before executing the specified function.
document.addEventListener('DOMContentLoaded', () => {
    const plusBtnContainer = document.querySelector('#basic-button');
    const container = document.querySelector('.ProjectDiv.row.flex-nowrap.overflow-auto.align-items-start');

    if (!plusBtnContainer || !container) {
        console.error('Plus button or container not found');
        return;
    }

    // find the actual inner "+" button
    const plusBtn = plusBtnContainer.querySelector('button');

    plusBtn.addEventListener('click', (e) => {
        //e.stopPropagation(); // prevent triggering onclick="showSectionModal()"
        const newColumn = document.createElement('div');
        newColumn.className = 'col-md-3 board-column';
        newColumn.innerHTML = `
            <h5 class="mb-3 text-white d-flex justify-content-between align-items-center">
                <span>New Section</span>
                <span class="d-flex gap-3">
                    <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor"
                         class="bi bi-plus add-column-plus" viewBox="0 0 16 16">
                        <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                    </svg>
                    <div class="btn-group">
                        <button type="button" class="btn p-0 m-0 bg-transparent border-0 dropdown-toggle"
                                data-bs-toggle="dropdown" aria-expanded="false">
                            <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor"
                                 class="bi bi-three-dots three-dots" viewBox="0 0 16 16">
                                <path d="M3 9.5a1.5 1.5 0 1 1 0-3
                                         1.5 1.5 0 0 1 0 3m5 0a1.5 1.5 0 1 1 0-3
                                         1.5 1.5 0 0 1 0 3m5 0a1.5 1.5 0 1 1 0-3
                                         1.5 1.5 0 0 1 0 3"/>
                            </svg>
                        </button>
                        <ul class="dropdown-menu dropdown-menu-end">
                            <li><a class="dropdown-item" href="#">Delete Section</a></li>
                        </ul>
                    </div>
                </span>
            </h5>
        `;

        container.insertBefore(newColumn, plusBtnContainer);
    });

     document.addEventListener('click', (event) => {
          // CORRECTION: Target the correct button class: .add-task-btn
          const addTaskButton = event.target.closest('.add-task-btn');

          if (addTaskButton) {
               const column = event.target.closest('.col-md-3.board-column');
               // Retrieve the section ID from the button
               const sectionId = addTaskButton.dataset.sectionId;
               showTaskModal(sectionId);     
               // 1. Create the new task UI element
               const newTask = document.createElement('div');
               newTask.className = 'card task-card p-3 mb-2'; // Added mb-2 for spacing
               newTask.innerHTML = `
                 <h6>New Task</h6>
                 <span class="text-xs">Due: --</span>
               `;

               // 2. Append the new task card to the section's column
               if (column) {
                    column.appendChild(newTask);
               }
          }
     });
});


 //what does a project modal do?
 //It allows users to create a new project by filling out a form.
 //so the current state of the modal 'newProjectModal' is hidden
 //we need to show it when the user clicks the "New Project" button
 function showProjectModal() {
      const modal = new bootstrap.Modal(document.getElementById('newProjectModal'));
      modal.show();
 }


 //what does bootstrap.Modal do?
 //It provides a way to create modal dialogs in Bootstrap.
 //but what is a modal
 //A modal is a dialog box/popup window that is displayed on top of the current page.
 function showSectionModal() {
      const modal = new bootstrap.Modal(document.getElementById('newSectionModal'));
      modal.show();
 }

 //function to show the hidden task modal
function showTaskModal(sectionId) {
     const modalElement = document.getElementById('newTaskModal');

     // Set the section ID on the hidden form element (or a hidden input field)
     const form = document.getElementById('create-task-form');
     form.dataset.targetSectionId = sectionId; // Using a dataset attribute on the form

     const modal = new bootstrap.Modal(modalElement);
     modal.show();
}
 //what does renderSections do?
 //It takes an array of section objects and dynamically creates HTML elements to display each section and its associated tasks on the webpage.
 //It first clears any existing sections (except the "Add Section" button) and then iterates through the sections array to create new columns for each section.
 //Each column includes the section name, a plus icon for adding tasks, and a three-dots icon for additional options (like deleting the section).
 //It also appends any tasks associated with each section inside the respective column.
  function renderSections(sections) {
       // Select the container where sections will be rendered
       const container = document.querySelector('.row.flex-nowrap.overflow-auto');

       // Clear existing sections except the "Add Section" button
       container.querySelectorAll('.board-column:not(#basic-button)').forEach(column => column.remove());

       // Iterate through sections and create HTML elements
       sections.forEach(section => {
            // Create column for each section
            // Create a new div element to represent the section column
            const sectionElement = document.createElement('div');
            sectionElement.className = 'col-md-3 board-column';

            //what does dataset do
            //It allows you to store custom data attributes on HTML elements.
            sectionElement.dataset.sectionId = section.id;
            sectionElement.innerHTML = `
                 <h5 class="mb-3 text-white d-flex justify-content-between align-items-center board-header">
                      <span>${section.name}</span>
                      <div class="d-flex gap-3 board-actions">
                           <button class="btn btn-sm p-0 m-0 bg-transparent border-0 add-task-btn" data-section-id="${section.id}">
                                <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" class="bi bi-plus" viewBox="0 0 16 16">
                                     <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                                </svg>
                           </button>
                           <div class="btn-group">
                                <button type="button" class="btn p-0 m-0 bg-transparent border-0 dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
                                     <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" class="bi bi-three-dots three-dots" viewBox="0 0 16 16">
                                     <path d="M3 9.5a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3m5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3m5 0a1.5 1.5 0 1 1 0-3 1.5 1.5 0 0 1 0 3"/>
                                     </svg>
                                </button>
                                <ul class="dropdown-menu dropdown-menu-end">
                                     <li><a class="dropdown-item delete-section-btn">Delete Section</a></li>
                                </ul>
                           </div>
                      </div>
                 </h5>
            `;

            // Append tasks inside this column
            section.tasks?.forEach(task => {
                 const taskCard = document.createElement('div');
                 taskCard.className = 'card task-card p-3 mb-2';
                 taskCard.innerHTML = `
                      <div class="d-flex justify-content-between align-items-center">
                           <h6>${task.title}</h6>
                           <div class="task-actions d-flex gap-2">
                                <button class="btn btn-sm p-0 m-0 bg-transparent border-0 add-subtask-btn" data-task-id="${task.id}" data-section-id="${section.id}" onclick="showTaskModal()">
                                     <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus" viewBox="0 0 16 16">
                                          <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                                     </svg>
                                </button>

                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-dash" viewBox="0 0 16 16">
                                     <path d="M4 8a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7A.5.5 0 0 1 4 8"/>
                                </svg>
                           </div>
                      </div>
                      <span class="text-xs">Due: ${task.dueDate || '--'}</span>    
                           `;
                 sectionElement.appendChild(taskCard);
            });
            // Insert the new section before the "Add Section" button
            //so it gets added to the container before the plus button
            //this way the plus button always stays at the end
            const plusBtn = document.getElementById('basic-button');
            container.insertBefore(sectionElement, plusBtn);
       });
  }




 async function fetchSectionsAndTasks() {
      const projectId = document.getElementById('project-data').dataset.projectId;
      console.log(projectId);
      const response = await fetch(`${project_app_url}/api/Workflow/GetAllSectionsFromProjectId?Id=${projectId}`);
      if (response.ok) {
           const sections = await response.json();
           renderSections(sections);
      } else {
           console.error("Failed to fetch sections & tasks");
      }
      console.log(response);
 }


 //so this function triggers every time user interacts with the section form
 document.addEventListener('DOMContentLoaded', () => {
      const form = document.getElementById('create-section-form');
      form.addEventListener('submit', async function (e) {
           // Prevent the default form submission behavior This prevents the page from reloading when the form is submitted.
           e.preventDefault();

           const sectionName = document.getElementById('SectionName').value.trim();
           const projectId = document.getElementById('project-data').dataset.projectId;

           if (!sectionName) return;

           // Create the section object to send to the API
           const section = {
                name: sectionName,
                projectId: projectId,
           };

           try {
                const response = await fetch('https://localhost:7006/api/Workflow/CreateSection', {
                     method: 'POST',
                     headers: {
                          'Content-Type': 'application/json'
                     },
                     body: JSON.stringify(section)
                });

                if (response.ok) {
                     await fetchSectionsAndTasks();
                     form.reset();
                     bootstrap.Modal.getInstance(document.getElementById('newSectionModal')).hide();
                     // renderSections();
                } else {
                     alert("Failed to create section. Please try again.");
                }
           } catch (error) {
                console.error("Error creating section:", error);
                alert("An unexpected error occurred.");
           }
      });
 });



 async function DeleteSectionAsync(sectionId) {
      const confirmed = confirm("Are you sure you want to delete this section?");
      if (!confirmed) return;

      const response = await fetch(`${project_app_url}/api/Workflow/DeleteSection?Id=${sectionId}`, {
           method: 'DELETE'
      });

      if (response.ok) {
           await fetchSectionsAndTasks();
      } else {
           alert('Failed to delete section.');
      }
 }

 document.addEventListener('click', function (event) {
      const deleteBtn = event.target.closest('.delete-section-btn');
      if (deleteBtn) {
           const column = deleteBtn.closest('.board-column');
           const sectionId = column?.dataset.sectionId;
           if (sectionId) {
                DeleteSectionAsync(sectionId);
           }
      }
 });



 // The async function to create the task
 async function createTasksAsync(sectionId,title, description = "this is a task") {
     try {
          const response = await fetch(`${project_app_url}/api/Workflow/CreateTask`, {
               method: 'POST',
               headers: {
                    'Content-Type': 'application/json'
               },
               body: JSON.stringify({
               title: title,
                    description: description,
                    sectionId: sectionId
               })
          });


          if (!response.ok) {
               alert("Failed to create section. Please try again.");
               throw new Error(`Failed to create task: ${response.statusText}`);
          }
          return await response.json();
     } catch (error) {
           console.error("Error creating task:", error);
      }
 }

document.addEventListener('DOMContentLoaded', () => {
     const form = document.getElementById('create-task-form');
     form.addEventListener('submit', async function (e) {
          // Prevent the default form submission behavior This prevents the page from reloading when the form is submitted.
          e.preventDefault();

          const taskName = document.getElementById('TaskName').value.trim();

          const sectionId = form.dataset.targetSectionId;
          console.log("Section Id:" + sectionId + " taskName = " + taskName);
          if (!taskName || !sectionId) { console.log("\nreturning null\n"); return; }


          try {
               const response = await createTasksAsync(sectionId,taskName);

               if (response) {
                    await fetchSectionsAndTasks();
                    form.reset();
                    bootstrap.Modal.getInstance(document.getElementById('newTaskModal')).hide();
               }
          } catch (error) {
               console.error("Error creating section:", error);
               alert("An unexpected error occurred.");
          }
     });
});


 //document.querySelector('.ProjectDiv').addEventListener('click', async (event) => {
 //    // Check if the clicked element or its parent is an "add-subtask-btn"
 //    const button = event.target.closest('.add-subtask-btn');
 //     console.log(button);
 //    if (button) {
 //        // Retrieve the section ID from the button's data attribute
 //        const sectionId = button.dataset.sectionId;
 //        const parentTaskId = button.dataset.taskId;
 //        const taskTitle = 'MyTask';
 //        const taskDescription = ''; // or grab from a form if you add one

 //        await createTasksAsync(sectionId, taskTitle, taskDescription);
 //    }
 //});



 // Initialize Feather Icons
 //but why replace 
 //because it replaces the <i> tags with SVG icons
 feather.replace();



