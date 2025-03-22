**Requerimiento completo**

Se desea desarrollar una aplicación que contenga una base de datos para guardar
la información sobre médicos, empleados y pacientes de un centro de salud.
* De los médicos se desea saber su nombre, dirección, teléfono, población,
provincia, código postal, NIF, número de la seguridad social, número de
colegiado y si es médico titular, médico interino o médico sustituto.
* Cada médico tiene un horario en el que pasa consulta, pudiendo ser diferente
cada día de la semana.
* Los datos de los médicos sustitutos no desaparecen cuando finalizan una
sustitución, se les da una fecha de baja.
* Así, cada sustituto puede tener varias fechas de alta y fechas de baja,
dependiendo de las sustituciones que haya realizado.
* Si la última fecha de alta es posterior a la última fecha de baja, el médico está
realizando una sustitución en la actualidad en el centro de salud.
* El resto de empleados son los ATS, ATS de zona, auxiliares de enfermería,
celadores y administrativos.
* De todos ellos se desea conocer su nombre, dirección, teléfono, población,
provincia, código postal, NIF y número de la seguridad social.
* De todos, médicos y empleados, se mantiene también información sobre los
períodos de vacaciones que tienen planificados y de los que ya han
disfrutado.
* Por último, de los pacientes se conoce su nombre, dirección, teléfono, código
postal, NIF, número de la seguridad social y médico que les corresponde.

**Modelo Entidad-Relacion**

![Entidad-Relacion-Clinica](https://github.com/user-attachments/assets/714f5ac3-c690-4253-a594-879ccdc4a810)
