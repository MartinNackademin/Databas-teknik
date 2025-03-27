const populateCustomersSelect = async () => 
{
        const res = await fetch('https://localhost:7272/api/Customers')
        const data = await res.json();
        
        const customersSelect = document.getElementById('customers');
        
        customersSelect.innerHTML = '<option disabled selected hidden value="0">Select a customer</option>';
        
        console.log(data);
        
        for (let customer of data) {
            customersSelect.innerHTML +=
             `<option value="${customer.id}">${customer.customerName}</option>`;
        
        }
}
const handleProjectRegistration = async (e) => 
{
            e.preventDefault();
        
            const form = e.target;
            const formData = {
                projectName: e.target['projectName'].value,
                projectDescription: e.target['description'].value,
                customerId: parseInt(e.target['customers'].value),
                startDate: e.target['startDate'].value,
                endDate: e.target['endDate'].value,
                projectStatus: e.target['status'].value,
            };
            
            console.log(formData);
            const res = await fetch('https://localhost:7272/api/projects', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(formData)
            });
            console.log(res.ok);

            if (res.ok) {
                alert('Project registered successfully');
                form.reset();
            }

}
const poplulateProjectList = async () => {
    const res = await fetch('https://localhost:7272/api/projects');
    const data = await res.json();
    const projectsElement = document.getElementById('projects');
    console.log(data);

    for (let project of data) {
        const formattedStartDate = new Date(project.startDate).toISOString().split('T')[0];
        const formattedEndDate = new Date(project.endDate).toISOString().split('T')[0];

        const projectDiv = document.createElement('div');
        projectDiv.classList.add('project');


        projectDiv.innerHTML = `
            <p>ID: ${project.id}</p>
            <p>Project Name: ${project.projectName}</p>
            <p>Customer Name: ${project.customer.customerName}</p>
            <p>Start Date: ${formattedStartDate}</p>
            <p>End Date: ${formattedEndDate}</p>
            <p>Status: ${project.projectStatus}</p>
        `;


        projectDiv.onclick = () => {
            window.location.href = `edit-project.html?id=${project.id}`;
        };

   
        projectsElement.appendChild(projectDiv);
    }
};

const handleProjectEdit = async (e) => { 
    e.preventDefault();

    const projectId = new URLSearchParams(window.location.search).get('id');
    if (!projectId || isNaN(projectId)) {
        alert('Invalid or missing project ID in URL');
        return;
    }
    alert('Project ID found:'+projectId);

    const form = e.target;
    const formData = {
        projectName: form['projectName'].value,
        projectDescription: form['description'].value,
        customerId: parseInt(form['customers'].value),
        startDate: new Date(form['startDate'].value),
        endDate: new Date(form['endDate'].value),
        projectStatus: form['status'].value,

    };


    console.log(formData);
    console.log(projectId);

    try {
        console.log(`PUT Request URL: https://localhost:7272/api/projects/${projectId}`);
        const res = await fetch(`https://localhost:7272/api/projects/1`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                
            },
            body: JSON.stringify(formData),
        });

        if (res.ok) {
            alert('Project updated successfully!');
            window.location.href = 'list-project.html';
        } else {
            throw new Error(`Failed to update project: ${res.status}`);
        }
    } catch (error) {
        console.error('Error updating project:', error);
    }
};

const populateProjectEditForm = async () => {
    const projectId = new URLSearchParams(window.location.search).get('id');
    if (!projectId || isNaN(projectId)) {
        alert('Invalid or missing project ID in URL');
        return;
    }

    const res = await fetch(`https://localhost:7272/api/projects/${projectId}`);
    const project = await res.json();
    console.log(project);

    await populateCustomersSelect();

    const form = document.getElementById('editProjectForm');

    form['projectName'].value = project.projectName;
    form['description'].value = project.projectDescription;
    form['customers'].value = project.customer.id; 
    form['startDate'].value = new Date(project.startDate).toISOString().split('T')[0];
    form['endDate'].value = new Date(project.endDate).toISOString().split('T')[0];
    form['status'].value = project.projectStatus;
}


