# 🚗 Crash Course: Multiplayer Car Crash Battle Royale

A real-time, physics-driven car crash battle royale built in Unity. This project showcases advanced multiplayer networking, collision physics, and performance optimization using C# and Unity's built-in systems — designed to simulate chaotic high-speed destruction in a scalable arena setting.

---

## 🎮 Gameplay Overview

In **Crash Course**, players control customizable vehicles in an open arena and battle against each other in a fast-paced demolition derby. The last car standing wins. Players can boost, ram, dodge, and outmaneuver their opponents in real-time, while the arena shrinks to increase intensity as the match progresses.

---

## 🚀 Features

- 🌐 **Multiplayer support** for up to 50 concurrent players (Steamworks SDK + AWS LightSail)
- 🏎 **Real-time vehicle physics** with high-fidelity collision handling and force response
- 🔥 **Damage calculation system** based on collision force vectors
- 💻 **C# optimized networking** with efficient packet handling and minimal latency (~150ms)
- 🎮 **Custom game lobby and matchmaking logic**
- 📊 **Telemetry and connection stability monitoring**
- 🔊 **Dynamic SFX and VFX** for immersive crash feedback
- 🏆 **Scalable game loop** supporting real-time events and round resets

---

## 🛠️ Tech Stack

| Tech              | Usage                                |
|-------------------|--------------------------------------|
| `Unity`           | Core game engine                     |
| `C#`              | Game logic and networking code       |
| `AWS LightSail`   | Dedicated server hosting             |
| `Steamworks SDK`  | Multiplayer networking + matchmaking |
| `Photon (optional)` | Networking experimentation         |
| `Unity Physics`   | Real-time collision & rigidbody logic|

---

## 📸 Screenshots

| In-Game Battle | Destruction Physics | Lobby System |
|----------------|--------------------|--------------|
| ![Gameplay](Assets/Screenshots/gameplay.png) | ![Physics](Assets/Screenshots/physics.png) | ![Lobby](Assets/Screenshots/lobby.png) |

> 📁 *Screenshots coming soon — replace placeholders after upload.*

---

## 🧪 How to Run

Clone the repository:

git clone https://github.com/MateenAminian/Crash-Course-Game.git

Open the project in Unity 2021.x+

Load the main `BattleRoyaleScene` located at `/Assets/Scenes/BattleRoyaleScene.unity`

Press ▶️ to play (single-player test mode)

**Note:** Multiplayer features require server config & Steamworks API keys to fully activate.

---

## 💡 Learning Goals

- Scalable multiplayer architecture
- Real-time physics simulation for destructible vehicles
- Custom packet design for low-latency data transfer
- Server-client architecture with state sync
- Arena shrinking mechanic and battle royale game design patterns
- Deployment on AWS LightSail for scalable hosting

---

## 👤 Author

**Mateen Aminian**  
Senior Software Engineer  
📍 Los Angeles, CA  
🔗 [Portfolio Website](https://YOURPORTFOLIO.link)  
📫 mateenaminian@gmail.com

---

## 📝 License

This project is open-source and for educational/demo purposes.  
All third-party assets remain under their original licenses.
