import React from "react";
import Ticket from "../components/Ticket";

const Project = () => {
    return (
        <div>
            <div className="flex flex-row-reverse md:-mt-10 mb-5">
                <button class="p-2 pl-5 pr-5 bg-blue-500 border-blue-300 text-gray-100 text-md rounded-lg focus:border-4 mr-14">Create Ticket</button>
                <button class="p-2 pl-5 pr-5 bg-yellow-600 border-yellow-300 text-gray-100 text-md rounded-lg focus:border-4 mr-5">Update Project</button>
            </div>

            <div className="flex flex-wrap justify-around justify-items-stretch h-4/5 mb-5" style={{ minHeight: "80vh" }}>
                <div className="bg-gray-400 border-2 border-black" style={{ width: "405px" }}>
                    <h2 className="text-center text-2xl border-b-2 border-black">TO-DOs</h2>
                    <div>
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={1}
                            assignee={"Isala Piyarisi"}
                        />
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={2}
                            assignee={"Isala Piyarisi"}
                        />
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={3}
                            assignee={"Isala Piyarisi"}
                        />
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={2}
                            assignee={"Isala Piyarisi"}
                        />
                    </div>
                </div>
                <div className="bg-yellow-100 border-2 border-black" style={{ width: "405px" }}>
                    <h2 className="text-center text-2xl border-b-2 border-black">In-Progress</h2>
                    <div>
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={2}
                            assignee={"Isala Piyarisi"}
                        />
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={1}
                            assignee={"Isala Piyarisi"}
                        />
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={3}
                            assignee={"Isala Piyarisi"}
                        />
                    </div>
                </div>
                <div className="bg-green-200 border-2 border-black" style={{ width: "405px" }}>
                    <h2 className="text-center text-2xl border-b-2 border-black">Completed</h2>
                    <div>
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={3}
                            assignee={"Isala Piyarisi"}
                        />
                        <Ticket
                            name={"Create Something Something"}
                            description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                            dueDate={"03/11/2021"}
                            estimatedTime={"15 Hours"}
                            prority={1}
                            assignee={"Isala Piyarisi"}
                        />
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Project;