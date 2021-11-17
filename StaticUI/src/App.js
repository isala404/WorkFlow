import './App.css';
import Navbar from './components/Navbar';
import { Routes, Route } from "react-router-dom";
import Home from './Pages/Home';
import Projects from './Pages/Projects';
import Reports from './Pages/Reports';
import Breadcrumbs from './components/Breadcrumbs';

function App() {
  return (
    <>
      <header>
        <Navbar options={[{name: "Home", href: "/"}, {name: "Projects", href: "/projects"}, {name: "Reports", href: "/reports"}]}/>
      </header>
      <div class="container mx-auto">
      <Breadcrumbs items={["Home"]} />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/projects" element={<Projects />} />
        <Route path="/reports" element={<Reports />} />
      </Routes>
      </div>

    </>
  );
}

export default App;
