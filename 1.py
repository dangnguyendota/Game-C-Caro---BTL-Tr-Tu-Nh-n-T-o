from cx_Freeze import setup, Executable
setup(
    name="GUI PROGRAM",
    version="0.1",
    description="MyEXE",
    executables=[Executable("E:\PycharmProjects\TriTueNhanTao/AI_test_3.py", base="Win32GUI")],
    )