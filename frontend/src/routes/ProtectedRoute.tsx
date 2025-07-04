// // src/routes/ProtectedRoute.jsx
// import { Navigate, Outlet } from 'react-router-dom';

// export default function ProtectedRoute({ allowedRoles, userRole }) {
//   if (!userRole) return <Navigate to="/login" />;

//   if (!allowedRoles.includes(userRole)) {
//     return <Navigate to="/unauthorized" />;
//   }

//   return <Outlet />;
// }
