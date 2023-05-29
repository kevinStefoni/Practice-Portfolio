import { Singleton } from "./components/Singleton";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/singleton',
    element: <Singleton />
  }
];

export default AppRoutes;
