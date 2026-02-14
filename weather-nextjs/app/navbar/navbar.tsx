import styles from "./navbar.module.css"

export default function navbar() {
  return (
    <nav className={styles.navbar}>
      <a href="./">Weather</a>
      <ul className={styles.pages}>
        <li><a href="#">Login</a></li>
        <li><a href="#">Register</a></li>
      </ul>
      <button className={styles.hamburger}>menu</button>
    </nav>
  );
}