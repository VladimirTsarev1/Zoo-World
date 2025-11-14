# Zoo World (Unity)

## Overview

Небольшой проект для демонстрации навыков в построении модульной сервисной архитектуры.  
Животные спавнятся во вьюпорте, двигаются по заданной стратегии и сталкиваются по правилам.  
В UI отображается количество съеденных животных.

---

## Architecture

### Composition Root

`GameRoot` — точка сборки проекта. Создаёт и связывает сервисы:

- `PoolService` - глобальный пул объектов
- `AnimalFactory` - создание животных
- `AnimalSpawnService` - асинхронный спавн животных
- `AnimalConfigService` - загрузка всех AnimalConfig
- `AnimalCollisionService` - обработка столкновений
- `AnimalViewportService` - разворот животных при выходе за экран
- `PopupService` - popup-лейблы при съедении животных
- `EatenAnimalsCounterService` - счётчики съеденных животных

---

## Animals

Каждое животное определяется **ScriptableObject-конфигом**:

- Tип (`Predator` / `Prey`)
- Cтратегия движения (`MoveConfig`)
- Ключ пула (`PoolKeyConfig`)

Поведение разделено:

- **Movement** - Strategy Pattern (`Linear` / `Jump`)
- **Collision** - ScriptableObject-обработчик поведения пар типов животных при столкновении (`PreyVsPrey`,
  `PredatorVsPrey`, `PredatorVsPredator`)
- **Viewport** - разворот при выходе за видимую область

---

## Pooling

Универсальный пул:

- `PoolService` хранит словарь `PoolKeyConfig -> Pool` для сопоставления ключей и пулов, и словарь `IPoolable -> Pool`
  для хранения активных обектов пула и сопоставления их с пулами
- Каждый `Pool` работает со своим префабом
- Поддерживаются режимы Release:
    - по таймеру
    - по OnDisable
    - вручную

---

## UI

UI состоит из:

- `EatenAnimalsCountersView` - отображает числа
- `EatenAnimalsCountersModel` - хранит данные
- `EatenAnimalsCountersPresenter` - связывает Model и View
- `EatenAnimalsCounterService` - служит для общения с Presenter'ом
- `PopupLabel` - лейбл, который создаётся через пул

---

## How to Run

1. Открыть сцену с `GameRoot`.
2. В инспекторе назначить:
    - `GameDataConfig`
    - Камеру
    - `EatenAnimalsCountersView`
    - `PoolKeyConfig` для popup-лейблов
3. Нажать **Play** — спавн начнётся автоматически.

---

## Extending

- **Добавить животное**: создать AnimalConfig, PoolConfig + PoolKeyConfig и prefab.
- **Добавить стратегию движение**: создать ScriptableObject-конфиг наследованный от `MoveConfig` и создать класс наследованный от `IMoveStrategy` содержащий логику передвижения.
- **Добавить новое столкновение**: создать ScriptableObject-обработчик наследованный от `AnimalCollisionConfig`.