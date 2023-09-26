import { Link } from "react-router-dom"
import styles from "./NavBar.module.scss"

const NavBar = () => {
  return (
    <nav className={styles.root}>
        <ul>
            <li><Link to="#">Home</Link></li>
            <li><Link to="#">Catalog</Link></li>
            <li><Link to="#">About</Link></li>
            <li><Link to="#">Contact us</Link></li>
        </ul>
    </nav>
  )
}

export default NavBar