import { Home } from "./components/Home";
import { Singleton } from "./components/Singleton";
import { Adapter } from "./components/Adapter";


const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/singleton',
    element: <Singleton />
    },
    {
        path: '/adapter',
        element: <Adapter />
    }
];

export default AppRoutes;
