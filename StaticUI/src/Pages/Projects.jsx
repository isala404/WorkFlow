import React from "react";
import { getCurrentCompany } from "../libs/getCompany";

const Home = () => {
    return (
        <div>
            <h1>Projects for {getCurrentCompany()}</h1>
        </div>
    )
}

export default Home;