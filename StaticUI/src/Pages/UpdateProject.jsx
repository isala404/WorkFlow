import React from "react";
import { getCurrentCompany } from "../libs/getCompany"

const UpdateProject = () => {
    return (
        <div className="flex flex-col lg:w-2/4 m-auto">
            <div className="flex flex-grow flex-col bg-gray-100 p-5 mt-20">
                <h1 className="text-2xl text-center">Update Richmond Academy Project</h1>
                <form>
                    <div class="mb-2">
                        <label for="name" class="text-grey-darker inline-block mb-2">Name</label>
                        <input type="text" id="name" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5" placeholder="My Project" required={true} />
                    </div>
                    <div class="mb-2">
                        <label for="status" class="text-grey-darker inline-block mb-2">Status</label>
                        <select id="status" className="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 bg-white" name="status" required>
                            <option value="todo">
                                To-Do
                            </option>
                            <option value="in-progress">
                                In-Progress
                            </option>
                            <option value="completed">
                                Completed
                            </option>
                        </select>
                    </div>
                    <div class="mb-2">
                        <label for="duedate" class="text-grey-darker inline-block mb-2">Due Date</label>
                        <input type="date" id="duedate" name="duedate" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full m-0 p-2.5" placeholder="31/10/2021 13:45" required={true} />
                    </div>
                    <label for="basic-url" class="text-grey-darker inline-block mb-2">Project URI</label>
                    <div class="flex flex-wrap items-stretch w-full mb-4 relative">
                        <div class="flex -mr-px">
                            <span class="flex items-center leading-normal bg-grey-lighter rounded rounded-r-none border border-r-0 border-grey-light px-3 whitespace-no-wrap text-grey-dark text-sm">https://workflow.isala.me/{getCurrentCompany()}/</span>
                        </div>
                        <input type="text" class="flex-shrink flex-grow flex-auto leading-normal w-px flex-1 border h-10 border-grey-light rounded rounded-l-none px-3 relative" pattern="[a-z0-9-]+" placeholder="my-project" />
                    </div>
                    <button type="submit" class="block text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 w-full">Submit</button>
                </form>
            </div>
            <div className="flex flex-grow flex-col bg-gray-100 p-5 w-full m-auto mt-10">
                <h1 className="text-2xl">Manage Users</h1>
                <form className="flex ml-auto -mt-8 mr-2 mb-3">
                    <input type="email" class="border-grey-light rounded rounded-lg px-3 w-48 mr-3" placeholder="user@example.com" required />
                    <button class="bg-blue-300 hover:bg-blue-400 font-semibold py-0 px-4 rounded inline-flex items-center">
                        <span>Add</span>
                    </button>
                </form>
                <User name="John Doe" />
                <User name="Jane Doe" />
                <User name="John Smith" />
                <User name="Jane Smith" />
            </div>
            <button class="self-end text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 mt-5 w-48">Delete Project</button>
        </div>
    )
}

const User = ({ name }) => {
    return (
        <div className="w-100 m-2 p-2 border border-black bg-white">
            {name}
            <button class="bg-red-500 text-white rounded-full h-6 px-3 float-right ml-3">Remove</button>
        </div>
    )
};


export default UpdateProject;