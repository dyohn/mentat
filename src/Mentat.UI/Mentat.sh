#!/usr/bin/env bash

CURRENT_DIR=$( cd -- "$( dirname -- "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )

MENTOR_DIR=${CURRENT_DIR} + /mentor
TEST_DIR=${CURRENT_DIR} + /tests
REPORT_DIR=${CURRENT_DIR} + /reports
DIFF_DIR=${REPORT_DIR}/diffs
SUBMIT_DIR=${CURRENT_DIR} + /submit
BUILD_DIR=${CURRENT_DIR} + /build
PROCESSED_DIR=${CURRENT_DIR} + /processed
HEADER=${CURRENT_DIR} + /header
SPACER="--------------------------------------------------------------------------
-************************************************************************-
--------------------------------------------------------------------------"

#Test the mentors build
echo "\e[0;32mBeginning running test against mentors sample: 111\e[0m"
cd ${TEST_DIR}
MENTOR =`echo "111" | cut -d'.' -f1`
cp 111 assignment
echo "\e[0;32mRunning the test: 1\e[0m"
1 >> ./${MENTOR_DIR}/${MENTOR}.txt
echo ${SPACER} >> ./${MENTOR_DIR}/${MENTOR}.txt
mv assignment ${MENTOR_DIR}/111

echo "\e[0;32mMentor testing complete.\e[0m"
echo "\e[0;32mBeginning student testing.\e[0m"
# Compile and move into test dir, setting up reports along the way.
cd ${SUBMIT_DIR}
for SOURCECODE in *; do
	STUDENT='echo "${SOURCECODE}" | cut -d'.' -f1'
	echo
	echo "\e[0;32mCompiling ${STUDENT}'s assignmnet...\e[0m"
	EXECUTABLE=${STUDENT}.x
	REPORT=${STUDENT}.txt
	cp ${HEADER} ${REPORT_DIR}/${REPORT}
	read -n 1 -r -s -p 'Press enter to continue...'
	echo
	gcc -std=c99 -o ${EXECUTABLE} ${SOURCECODE}
	mv ${EXECUTABLE} ${TEST_DIR}/assignment
	cd ${TEST_DIR}
	echo "\e[0;32mRunning the test: 1\e[0m"
	timeout 10 1 >> ${REPORT_DIR}/${REPORT}
	echo ${SPACER} >> ${REPORT_DIR}/${REPORT}
	echo "\e[0;32mStudent testing complete.\e[0m"
	echo "\e[0;32mBeginning clean up.\e[0m"
	
	# Clean up
	echo "\e[0;32mMoving assignment to ${BUILD_DIR}/${EXECUTABLE}...\e[0m"
	mv assignment ${BUILD_DIR}/${EXECUTABLE}
	echo "\e[0;32mMoving #{SUBMIT_DIR}/${SOURCECODE} ${PROCESSED_DIR}/${SOURCECODE}...\e[0m"
	mv ${SUBMIT_DIR}/${SOURCECODE} ${PROCESSED_DIR}/${SOURCECODE}
	diff -u ${MENTOR_DIR}/${MENTOR}.txt ${REPORT_DIR}/${REPORT} > ${DIFF_DIR}/${STUDENT}_diff.txt
	cd ${SUBMIT_DIR}
	echo
	echo "\e[0;32mCompleting grading ${STUDENT}...\e[0m"
	echo
done