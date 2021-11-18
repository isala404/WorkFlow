import './App.css';
import Navbar from './components/Navbar';
import { Routes, Route } from "react-router-dom";
import Home from './Pages/Home';
import Projects from './Pages/Projects';
import Project from './Pages/Project';
import Reports from './Pages/Reports';
import UserSettings from './Pages/UserSettings';
import CreateCompany from './Pages/CreateCompany';
import UpdateCompany from './Pages/UpdateCompany';
import CreateProject from './Pages/CreateProject';
import UpdateProject from './Pages/UpdateProject';
import Breadcrumbs from './components/Breadcrumbs';

function App() {
  return (
    <>
      <header>
        <Navbar options={[{name: "Home", href: "/"}, {name: "Projects", href: "/projects"}, {name: "Reports", href: "/reports"}]}/>
      </header>
      <div className="container mx-auto">
      <Breadcrumbs />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/:company/edit" element={<UpdateCompany />} />
        <Route path="/:company/projects" element={<Projects />} />
        <Route path="/:company/projects/:project" element={<Project />} />
        <Route path="/:company/projects/create" element={<CreateProject />} />
        <Route path="/:company/projects/:project/edit" element={<UpdateProject />} />
        <Route path="/:company/reports" element={<Reports />} />
        <Route path="/user/settings" element={<UserSettings />} />
        <Route path="/create/company" element={<CreateCompany />} />
      </Routes>
      </div>

    </>
  );
}

export default App;
