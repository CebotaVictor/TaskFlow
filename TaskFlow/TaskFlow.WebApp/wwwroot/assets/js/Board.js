document.addEventListener('DOMContentLoaded', fetchSectionsAndTasks);

document.addEventListener('DOMContentLoaded', () => {
     const plusBtn = document.querySelector('#basic-button');
     const container = document.querySelector('.row.flex-nowrap.overflow-auto');

     // Add new column when clicking the main plus button
     plusBtn.addEventListener('click', () => {
          const newColumn = document.createElement('div');
          newColumn.className = 'col-md-3 board-column';
          newColumn.innerHTML = `
                <h5 class="mb-3 text-white d-flex justify-content-between align-items-center">
                    <span>To Do</span>
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

          container.insertBefore(newColumn, plusBtn);
     });

     // Listen for clicks on dynamically added ".add-column-plus" buttons (event delegation)
     document.addEventListener('click', (event) => {
          if (event.target.classList.contains('add-column-plus')) {
               const column = event.target.closest('.col-md-3.board-column');

               const newTask = document.createElement('div');
               newTask.className = 'card task-card p-3';
               newTask.innerHTML = `
                  <h6>New Task</h6>
                  <span class="text-xs">Due: --</span>
                `;

               column.appendChild(newTask);
          }
     });
});

function showProjectModal() {
     const modal = new bootstrap.Modal(document.getElementById('newProjectModal'));
     modal.show();
}

function showSectionModal() {
     const modal = new bootstrap.Modal(document.getElementById('newSectionModal'));
     modal.show();
}

function renderSections(sections) {
     const container = document.querySelector('.row.flex-nowrap.overflow-auto');

     container.querySelectorAll('.board-column:not(#basic-button)').forEach(column => column.remove());


     sections.forEach(section => {
          const sectionElement = document.createElement('div');
          sectionElement.className = 'col-md-3 board-column';

          sectionElement.dataset.sectionId = section.id;

          sectionElement.innerHTML = `
                    <h5 class="mb-3 text-white d-flex justify-content-between align-items-center">
                      <span>${section.name}</span>
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
                             <li><a class="dropdown-item delete-section-btn" >Delete Section</a></li>
                           </ul>
                    </div>
                    </span>
                    </h5>
                    `;

          // Append tasks inside this column
          section.tasks?.forEach(task => {
               const taskCard = document.createElement('div');
               taskCard.className = 'card task-card p-3 mb-2';
               taskCard.innerHTML = `
                          <h6>${task.name}</h6>
                          <span class="text-xs">Due: ${task.dueDate || '--'}</span>
                      `;
               sectionElement.appendChild(taskCard);
          });

          // Insert the column before the plus button
          const plusBtn = document.getElementById('basic-button');
          container.insertBefore(sectionElement, plusBtn);
     });
}


async function fetchSectionsAndTasks() {
     const projectId = document.getElementById('project-data').dataset.projectId;
     const response = await fetch(`https://localhost:7006/api/Workflow/GetAllSectionsFromProjectId?Id=${projectId}`);
     if (response.ok) {
          const sections = await response.json();
          renderSections(sections);
     } else {
          console.error("Failed to fetch sections & tasks");
     }
     console.log(response);
}

async function CreateSection() {

}

async function DeleteSectionAsync(sectionId) {
     const confirmed = confirm("Are you sure you want to delete this section?");
     if (!confirmed) return;

     const response = await fetch(`https://localhost:7006/api/Workflow/DeleteSection?Id=${sectionId}`, {
          method: 'DELETE'
     });

     if (response.ok) {
          await fetchSectionsAndTasks(); // Refresh after delete
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


document.addEventListener('DOMContentLoaded', () => {
     const form = document.getElementById('create-section-form');
     form.addEventListener('submit', function (e) {
          e.preventDefault(); // Stop actual form submission

          const sectionName = document.getElementById('SectionName').value.trim();
          if (!sectionName) return;

          const section = {
               id: 0,
               name: sectionName,
               tasks: []
          };

          renderSections([section]);

          // Reset form and close modal
          form.reset();
          bootstrap.Modal.getInstance(document.getElementById('newSectionModal')).hide();
     });
});


