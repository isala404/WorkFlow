import React from "react";

const Home = () => {
    return (
        <div className="flex flex-grow flex-col bg-gray-100 p-5 md:w-4/5 xl:w-2/5 m-auto mt-20">
            <h1 className="text-2xl text-center">Create a new ticket</h1>
            <form>
                <div class="mb-2">
                    <label for="name" class="text-grey-darker inline-block mb-2">Name</label>
                    <input type="text" id="name" name="name" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5" placeholder="Ticket Name" required={true} />
                </div>
                <div class="mb-2">
                    <label for="description" class="text-grey-darker inline-block mb-2">Description</label>
                    <textarea id="description" name="description" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5" placeholder="Ticket description" required={true} />
                </div>
                <div class="mb-2">
                    <label for="priority" class="text-grey-darker inline-block mb-2">Priority</label>
                    <select id="priority" className="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 bg-white" name="priority" required>
                        <option value="high">
                            High
                        </option>
                        <option value="medium">
                            Medium
                        </option>
                        <option value="low">
                            Low
                        </option>
                    </select>
                </div>
                <div class="mb-2">
                    <label for="assignee" class="text-grey-darker inline-block mb-2">Assignee</label>
                    <select id="assignee" className="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 bg-white" name="assignee" required>
                        <option value="">
                            Select Assignee
                        </option>
                        <option value="John">
                            John Doe
                        </option>
                        <option value="Jane">
                            Jane Doe
                        </option>
                        <option value="John">
                            John Smith
                        </option>
                    </select>
                </div>
                <div className="flex flex-row justify-between mb-2">
                    <div className="w-full mr-3">
                        <label for="duedate" class="text-grey-darker inline-block mb-2">Due Date</label>
                        <input type="text" id="duedate" name="duedate" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full m-0 p-2.5" placeholder="31/10/2021 13:45" required={true} />
                    </div>
                    <div className="w-full ml-3">
                        <label for="eta" class="text-grey-darker inline-block mb-2">Estimated time</label>
                        <input type="text" id="eta" name="eta" class="border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full m-0 p-2.5" placeholder="15 hours" required={true} />
                    </div>
                </div>
                <button type="submit" class="block text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 w-full">Submit</button>
            </form>
        </div>
    )
}

export default Home;