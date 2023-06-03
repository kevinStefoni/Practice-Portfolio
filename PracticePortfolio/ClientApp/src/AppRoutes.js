import { Home } from "./components/Home";
import { Singleton } from "./components/Singleton";
import { Adapter } from "./components/Adapter";
import { ExplicitOperator } from "./components/ExplicitOperator";
import { ImplicitOperator } from "./components/ImplicitOperator";


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
    },
    {
        path: '/explicit-operator',
        element: <ExplicitOperator />
    },
    {
        path: '/implicit-operator',
        element: <ImplicitOperator />
    }
];

export default AppRoutes;
