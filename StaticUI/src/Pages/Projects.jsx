import React from "react";

const Home = () => {
    return (
        <div className="container">
            <h1 className="inline text-4xl mt-8">Projects</h1>
            <a href="projects/create"><button class="p-2 pl-5 pr-5 bg-blue-500 text-gray-100 text-md rounded-lg focus:border-4 border-blue-300 float-right">Create Project</button></a>
            <table className="table-auto border mt-10 xl:text-lg container">
                <thead className="border bg-gray-50 ">
                    <tr className="border">
                        <th className="border px-4 py-1 text-left">Project ID</th>
                        <th className="border px-4 py-1 text-left">Project Name</th>
                        <th className="border px-4 py-1 text-left">Due date</th>
                        <th className="border px-4 py-1 text-left">To-Do Tickets</th>
                        <th className="border px-4 py-1 text-left">In-Progress Tickets</th>
                        <th className="border px-4 py-1 text-left">Completed Tickets</th>
                        <th className="border px-4 py-1 text-left">Total Assignees</th>
                        <th className="border px-4 py-1 text-left">Status</th>
                        <th className="border px-4 py-1 text-left">View</th>
                    </tr>
                </thead>
                <tbody>
                    {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(i => (
                        <tr>
                            <td className="border px-4 py-1">mH37sbw7</td>
                            <td className="border px-4 py-1">Richmond Academy</td>
                            <td className="border px-4 py-1">10/12/2021</td>
                            <td className="border px-4 py-1">{i}</td>
                            <td className="border px-4 py-1">2</td>
                            <td className="border px-4 py-1">1</td>
                            <td className="border px-4 py-1">3</td>
                            <td className="border px-4 py-1">In-Progress</td>
                            <td className="border px-4 py-1">
                                <a href="projects/Richmond Academy"><svg xmlns="http://www.w3.org/2000/svg" className="h-5 w-5" viewBox="0 0 20 20" fill="currentColor">
                                    <path d="M11 3a1 1 0 100 2h2.586l-6.293 6.293a1 1 0 101.414 1.414L15 6.414V9a1 1 0 102 0V4a1 1 0 00-1-1h-5z" />
                                    <path d="M5 5a2 2 0 00-2 2v8a2 2 0 002 2h8a2 2 0 002-2v-3a1 1 0 10-2 0v3H5V7h3a1 1 0 000-2H5z" />
                                </svg></a>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default Home;