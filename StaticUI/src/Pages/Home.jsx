import React from "react";
import Ticket from "../components/Ticket";

const Home = () => {
    return (
        <div class="bg-gray-100 p-8">
            <div>
                <h2 className="text-2xl">Today</h2>
                <div className="whitespace-nowrap overflow-x-auto">
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={1}
                        assignee={"Isala Piyarisi"}
                        status={'todo'}
                    />
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={2}
                        assignee={"Isala Piyarisi"}
                        status={'inprogress'}
                    />
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={3}
                        assignee={"Isala Piyarisi"}
                        status={'done'}
                    />
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={2}
                        assignee={"Isala Piyarisi"}
                        status={'done'}
                    />
                </div>
            </div>
            <div>
                <h2 className="text-2xl">Overdue</h2>
                <div className="whitespace-nowrap overflow-x-auto">
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={3}
                        assignee={"Isala Piyarisi"}
                        status={'todo'}
                    />
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={1}
                        assignee={"Isala Piyarisi"}
                        status={'inprogress'}
                    />
                </div>
            </div>
            <div>
                <h2 className="text-2xl">Next</h2>
                <div className="whitespace-nowrap overflow-x-auto">
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={2}
                        assignee={"Isala Piyarisi"}
                        status={'todo'}
                    />
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={1}
                        assignee={"Isala Piyarisi"}
                        status={'inprogress'}
                    />
                    <Ticket
                        name={"Create Something Something"}
                        description={"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce et sapien pulvinar ante pellentesque lacinia. Etiam tincidunt sed...."}
                        dueDate={"03/11/2021"}
                        estimatedTime={"15 Hours"}
                        prority={3}
                        assignee={"Isala Piyarisi"}
                        status={'done'}
                    />
                </div>
            </div>
        </div>
    )
}

export default Home;